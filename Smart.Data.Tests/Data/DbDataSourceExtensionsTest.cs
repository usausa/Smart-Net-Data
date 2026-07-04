namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Smart.Data.Mocks;

public sealed class DbDataSourceExtensionsTest
{
    //--------------------------------------------------------------------------------
    // Using
    //--------------------------------------------------------------------------------

    [Fact]
    public void UsingOpensAndDisposesConnection()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder);
        var captured = default(DbConnection?);

        dataSource.Using(con => { captured = con; });

        Assert.NotNull(captured);
        Assert.Equal(1, recorder.OpenSync);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public async Task UsingAsyncPropagatesTokenToOpen()
    {
        var recorder = new Recorder();
        await using var dataSource = new RecordingDbDataSource(recorder);
        using var cts = new CancellationTokenSource();

        await dataSource.UsingAsync(_ => ValueTask.CompletedTask, cts.Token).ConfigureAwait(true);

        Assert.Equal(1, recorder.OpenAsync);
        Assert.Equal(cts.Token, recorder.OpenToken);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public void UsingWhenOpenFailsDisposesConnectionAndThrows()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder, failOnOpen: true);

        Assert.Throws<InvalidOperationException>(() => dataSource.Using(_ => { }));

        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    //--------------------------------------------------------------------------------
    // Transaction
    //--------------------------------------------------------------------------------

    [Fact]
    public void UsingAutoTxCommitsOnSuccess()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder);

        dataSource.UsingAutoTx((_, _) => { });

        Assert.Equal(1, recorder.Commit);
        Assert.Equal(1, recorder.TransactionDisposed);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public void UsingAutoTxDoesNotCommitOnException()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder);

        Assert.Throws<InvalidOperationException>(() => dataSource.UsingAutoTx((_, _) => throw new InvalidOperationException()));

        Assert.Equal(0, recorder.Commit);
        Assert.Equal(1, recorder.TransactionDisposed);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public async Task UsingAutoTxAsyncCommitsWithToken()
    {
        var recorder = new Recorder();
        await using var dataSource = new RecordingDbDataSource(recorder);
        using var cts = new CancellationTokenSource();

        await dataSource.UsingAutoTxAsync((_, _) => ValueTask.CompletedTask, cts.Token).ConfigureAwait(true);

        Assert.Equal(1, recorder.Commit);
        Assert.Equal(cts.Token, recorder.CommitToken);
        Assert.Equal(cts.Token, recorder.BeginToken);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingTxAsyncWithIsolationLevelPassesLevel()
    {
        var recorder = new Recorder();
        await using var dataSource = new RecordingDbDataSource(recorder);

        await dataSource.UsingTxAsync(IsolationLevel.Serializable, (_, _) => ValueTask.CompletedTask).ConfigureAwait(true);

        Assert.Equal(IsolationLevel.Serializable, recorder.BeginLevel);
        Assert.Equal(0, recorder.Commit);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

    //--------------------------------------------------------------------------------
    // Batch
    //--------------------------------------------------------------------------------

    [Fact]
    public void UsingTxBatchAssignsTransactionToBatch()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder);

        dataSource.UsingTxBatch((_, _, _) => { });

        Assert.Equal(1, recorder.BatchCreated);
        Assert.Equal(1, recorder.BatchTransactionSet);
        Assert.Equal(0, recorder.Commit);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingAutoTxBatchAsyncCommits()
    {
        var recorder = new Recorder();
        await using var dataSource = new RecordingDbDataSource(recorder);

        await dataSource.UsingAutoTxBatchAsync((_, _, _) => ValueTask.CompletedTask).ConfigureAwait(true);

        Assert.Equal(1, recorder.BatchCreated);
        Assert.Equal(1, recorder.BatchTransactionSet);
        Assert.Equal(1, recorder.Commit);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

    //--------------------------------------------------------------------------------
    // Defer / cancellation propagation
    //--------------------------------------------------------------------------------

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingDeferAsyncEnumerableObservesCancellationBetweenItems()
    {
        var recorder = new Recorder();
        await using var dataSource = new RecordingDbDataSource(recorder);
        using var cts = new CancellationTokenSource();
        var source = new[] { 1, 2, 3 };

        // ReSharper disable MethodSupportsCancellation
        var e = dataSource.UsingDeferAsync(_ => new ValueTask<IEnumerable<int>>(source), cts.Token).GetAsyncEnumerator();
        // ReSharper restore MethodSupportsCancellation
        try
        {
            Assert.True(await e.MoveNextAsync().ConfigureAwait(true));
            Assert.Equal(1, e.Current);

            await cts.CancelAsync().ConfigureAwait(true);

            await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await e.MoveNextAsync().ConfigureAwait(true)).ConfigureAwait(true);
        }
        finally
        {
            await e.DisposeAsync().ConfigureAwait(true);
        }
    }
#pragma warning restore xUnit1051

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingDeferAsyncAsyncEnumerablePropagatesCancellationToSource()
    {
        var recorder = new Recorder();
        await using var dataSource = new RecordingDbDataSource(recorder);
        using var cts = new CancellationTokenSource();

        // ReSharper disable MethodSupportsCancellation
        var e = dataSource.UsingDeferAsync(_ => CancelableSource(), cts.Token).GetAsyncEnumerator();
        // ReSharper restore MethodSupportsCancellation
        try
        {
            Assert.True(await e.MoveNextAsync().ConfigureAwait(true));
            Assert.Equal(1, e.Current);

            await cts.CancelAsync().ConfigureAwait(true);

            await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await e.MoveNextAsync().ConfigureAwait(true)).ConfigureAwait(true);
        }
        finally
        {
            await e.DisposeAsync().ConfigureAwait(true);
        }
    }
#pragma warning restore xUnit1051

    private static async IAsyncEnumerable<int> CancelableSource([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        yield return 1;
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();
        yield return 2;
    }
}

namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Smart.Data.Mocks;

public sealed class DbProviderExtensionsTest
{
    private static readonly int[] DeferItems = [1, 2, 3];

    //--------------------------------------------------------------------------------
    // Using
    //--------------------------------------------------------------------------------

    [Fact]
    public void UsingOpensAndDisposesConnection()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);
        var captured = default(DbConnection?);

        provider.Using(con => { captured = con; });

        Assert.NotNull(captured);
        Assert.Equal(1, recorder.OpenSync);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingAsyncOpensAndDisposesConnection()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        await provider.UsingAsync(_ => ValueTask.CompletedTask).ConfigureAwait(true);

        Assert.Equal(1, recorder.OpenAsync);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

    [Fact]
    public async Task UsingAsyncWithTokenPropagatesTokenToOpen()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);
        using var cts = new CancellationTokenSource();

        await provider.UsingAsync(_ => ValueTask.CompletedTask, cts.Token).ConfigureAwait(true);

        Assert.Equal(1, recorder.OpenAsync);
        Assert.Equal(cts.Token, recorder.OpenToken);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public void UsingWhenOpenFailsDisposesConnectionAndThrows()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder, failOnOpen: true);
        var called = false;

        Assert.Throws<InvalidOperationException>(() => provider.Using(_ => { called = true; }));

        Assert.False(called);
        Assert.Equal(1, recorder.OpenSync);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingAsyncWhenOpenFailsDisposesConnectionAndThrows()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder, failOnOpen: true);
        var called = false;

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await provider.UsingAsync(_ =>
        {
            called = true;
            return ValueTask.CompletedTask;
        }).ConfigureAwait(true)).ConfigureAwait(true);

        Assert.False(called);
        Assert.Equal(1, recorder.OpenAsync);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

    [Fact]
    public async Task UsingAsyncWhenTokenCanceledThrowsAndDisposes()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync().ConfigureAwait(true);
        var called = false;

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await provider.UsingAsync(
            _ =>
            {
                called = true;
                return ValueTask.CompletedTask;
            },
            cts.Token).ConfigureAwait(true)).ConfigureAwait(true);

        Assert.False(called);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    //--------------------------------------------------------------------------------
    // Transaction
    //--------------------------------------------------------------------------------

    [Fact]
    public void UsingTxBeginsTransactionWithoutCommit()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        provider.UsingTx((_, _) => { });

        Assert.Equal(1, recorder.BeginSync);
        Assert.Equal(0, recorder.Commit);
        Assert.Equal(1, recorder.TransactionDisposed);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public void UsingAutoTxCommitsOnSuccess()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        provider.UsingAutoTx((_, _) => { });

        Assert.Equal(1, recorder.Commit);
        Assert.Equal(1, recorder.TransactionDisposed);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public void UsingAutoTxDoesNotCommitOnException()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        Assert.Throws<InvalidOperationException>(() => provider.UsingAutoTx((_, _) => throw new InvalidOperationException()));

        Assert.Equal(0, recorder.Commit);
        Assert.Equal(1, recorder.TransactionDisposed);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public async Task UsingAutoTxAsyncCommitsWithToken()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);
        using var cts = new CancellationTokenSource();

        await provider.UsingAutoTxAsync((_, _) => ValueTask.CompletedTask, cts.Token).ConfigureAwait(true);

        Assert.Equal(1, recorder.Commit);
        Assert.Equal(cts.Token, recorder.CommitToken);
        Assert.Equal(cts.Token, recorder.BeginToken);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingAutoTxAsyncDoesNotCommitOnException()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await provider.UsingAutoTxAsync((_, _) => throw new InvalidOperationException()).ConfigureAwait(true)).ConfigureAwait(true);

        Assert.Equal(0, recorder.Commit);
        Assert.Equal(1, recorder.TransactionDisposed);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingTxAsyncWithIsolationLevelPassesLevel()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        await provider.UsingTxAsync(IsolationLevel.Serializable, (_, _) => ValueTask.CompletedTask).ConfigureAwait(true);

        Assert.Equal(IsolationLevel.Serializable, recorder.BeginLevel);
        Assert.Equal(0, recorder.Commit);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

    //--------------------------------------------------------------------------------
    // Batch
    //--------------------------------------------------------------------------------

    [Fact]
    public void UsingBatchCreatesAndDisposesBatch()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        provider.UsingBatch((_, _) => { });

        Assert.Equal(1, recorder.BatchCreated);
        Assert.Equal(1, recorder.BatchDisposed);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    [Fact]
    public void UsingTxBatchAssignsTransactionToBatch()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        provider.UsingTxBatch((_, _, _) => { });

        Assert.Equal(1, recorder.BeginSync);
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
        var provider = new RecordingDbProvider(recorder);

        await provider.UsingAutoTxBatchAsync((_, _, _) => ValueTask.CompletedTask).ConfigureAwait(true);

        Assert.Equal(1, recorder.BatchCreated);
        Assert.Equal(1, recorder.BatchTransactionSet);
        Assert.Equal(1, recorder.Commit);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

    //--------------------------------------------------------------------------------
    // Defer / cancellation propagation
    //--------------------------------------------------------------------------------

    [Fact]
    public void UsingDeferEnumeratesThenDisposes()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        var result = new List<int>();
        foreach (var item in provider.UsingDefer(_ => DeferItems))
        {
            Assert.Equal(0, recorder.ConnectionDisposed);
            result.Add(item);
        }

        Assert.Equal(DeferItems, result);
        Assert.Equal(1, recorder.OpenSync);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }

    // ValueTask<IEnumerable<T>> overload: the synchronous foreach must observe cancellation between items (review B-1).
#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingDeferAsyncEnumerableObservesCancellationBetweenItems()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);
        using var cts = new CancellationTokenSource();
        var source = new[] { 1, 2, 3 };

        // ReSharper disable MethodSupportsCancellation
        var e = provider.UsingDeferAsync(_ => new ValueTask<IEnumerable<int>>(source), cts.Token).GetAsyncEnumerator();
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

    // IAsyncEnumerable<T> overload: the token must flow into the source via WithCancellation (review A-3).
#pragma warning disable xUnit1051
    [Fact]
    public async Task UsingDeferAsyncAsyncEnumerablePropagatesCancellationToSource()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);
        using var cts = new CancellationTokenSource();

        // ReSharper disable MethodSupportsCancellation
        var e = provider.UsingDeferAsync(_ => CancelableSource(), cts.Token).GetAsyncEnumerator();
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

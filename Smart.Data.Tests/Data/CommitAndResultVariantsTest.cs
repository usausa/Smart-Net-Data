namespace Smart.Data;

using System.Threading;
using System.Threading.Tasks;

using Smart.Data.Mocks;

public sealed class CommitAndResultVariantsTest
{
    //--------------------------------------------------------------------------------
    // Provider: result / state flow
    //--------------------------------------------------------------------------------

    [Fact]
    public void ProviderUsingAutoTxFuncCommitsAndReturnsResult()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        var result = provider.UsingAutoTx((_, _) => 42);

        Assert.Equal(42, result);
        Assert.Equal(1, recorder.Commit);
    }

    [Fact]
    public void ProviderUsingAutoTxFuncWithStatePassesStateAndCommits()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        var result = provider.UsingAutoTx(7, (_, _, state) => state * 2);

        Assert.Equal(14, result);
        Assert.Equal(1, recorder.Commit);
    }

    [Fact]
    public async Task ProviderUsingAutoTxAsyncFuncCommitsAndReturnsResult()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        var result = await provider.UsingAutoTxAsync((_, _) => new ValueTask<int>(99)).ConfigureAwait(true);

        Assert.Equal(99, result);
        Assert.Equal(1, recorder.Commit);
    }

    [Fact]
    public void ProviderUsingAutoTxBatchCommits()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        provider.UsingAutoTxBatch((_, _, _) => { });

        Assert.Equal(1, recorder.BatchCreated);
        Assert.Equal(1, recorder.BatchTransactionSet);
        Assert.Equal(1, recorder.Commit);
    }

    [Fact]
    public async Task ProviderUsingAutoTxBatchAsyncFuncCommitsAndReturnsResult()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        var result = await provider.UsingAutoTxBatchAsync((_, _, _) => new ValueTask<int>(5)).ConfigureAwait(true);

        Assert.Equal(5, result);
        Assert.Equal(1, recorder.Commit);
        Assert.Equal(1, recorder.BatchTransactionSet);
    }

    [Fact]
    public void ProviderUsingTxFuncDoesNotCommitButReturnsResult()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        var result = provider.UsingTx((_, _) => 3);

        Assert.Equal(3, result);
        Assert.Equal(0, recorder.Commit);
    }

    //--------------------------------------------------------------------------------
    // Provider: non-CT async overloads funnel CancellationToken.None
    //--------------------------------------------------------------------------------

#pragma warning disable xUnit1051
    [Fact]
    public async Task ProviderUsingAsyncNonCtOpensWithNoneToken()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        await provider.UsingAsync(_ => ValueTask.CompletedTask).ConfigureAwait(true);

        Assert.Equal(1, recorder.OpenAsync);
        Assert.Equal(CancellationToken.None, recorder.OpenToken);
        Assert.Equal(1, recorder.ConnectionDisposed);
    }
#pragma warning restore xUnit1051

#pragma warning disable xUnit1051
    [Fact]
    public async Task ProviderUsingAutoTxAsyncNonCtCommitsWithNoneToken()
    {
        var recorder = new Recorder();
        var provider = new RecordingDbProvider(recorder);

        await provider.UsingAutoTxAsync((_, _) => ValueTask.CompletedTask).ConfigureAwait(true);

        Assert.Equal(1, recorder.Commit);
        Assert.Equal(CancellationToken.None, recorder.CommitToken);
    }
#pragma warning restore xUnit1051

    //--------------------------------------------------------------------------------
    // DataSource: result / state flow
    //--------------------------------------------------------------------------------

    [Fact]
    public void DataSourceUsingAutoTxFuncCommitsAndReturnsResult()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder);

        var result = dataSource.UsingAutoTx((_, _) => 21);

        Assert.Equal(21, result);
        Assert.Equal(1, recorder.Commit);
    }

    [Fact]
    public void DataSourceUsingAutoTxBatchCommits()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder);

        dataSource.UsingAutoTxBatch((_, _, _) => { });

        Assert.Equal(1, recorder.Commit);
        Assert.Equal(1, recorder.BatchTransactionSet);
    }

#pragma warning disable xUnit1051
    [Fact]
    public async Task DataSourceUsingAutoTxBatchAsyncFuncCommitsAndReturnsResult()
    {
        var recorder = new Recorder();
        await using var dataSource = new RecordingDbDataSource(recorder);

        var result = await dataSource.UsingAutoTxBatchAsync((_, _, _) => new ValueTask<int>(8)).ConfigureAwait(true);

        Assert.Equal(8, result);
        Assert.Equal(1, recorder.Commit);
    }
#pragma warning restore xUnit1051

    [Fact]
    public void DataSourceUsingTxFuncDoesNotCommitButReturnsResult()
    {
        var recorder = new Recorder();
        using var dataSource = new RecordingDbDataSource(recorder);

        var result = dataSource.UsingTx((_, _) => 3);

        Assert.Equal(3, result);
        Assert.Equal(0, recorder.Commit);
    }
}

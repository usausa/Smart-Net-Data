namespace Smart.Data.Mocks;

using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

internal sealed class RecordingDbTransaction : DbTransaction
{
    private readonly DbConnection connection;

    private readonly Recorder recorder;

    private readonly IsolationLevel isolationLevel;

    private bool completed;

    private bool disposed;

    public RecordingDbTransaction(DbConnection connection, Recorder recorder, IsolationLevel isolationLevel)
    {
        this.connection = connection;
        this.recorder = recorder;
        this.isolationLevel = isolationLevel;
    }

    public override IsolationLevel IsolationLevel => isolationLevel;

    protected override DbConnection? DbConnection => connection;

    public override void Commit()
    {
        recorder.Commit++;
        completed = true;
    }

    public override void Rollback()
    {
        recorder.Rollback++;
        completed = true;
    }

    public override Task CommitAsync(CancellationToken cancellationToken)
    {
        recorder.Commit++;
        recorder.CommitToken = cancellationToken;
        completed = true;
        return Task.CompletedTask;
    }

    public override Task RollbackAsync(CancellationToken cancellationToken)
    {
        recorder.Rollback++;
        completed = true;
        return Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && !disposed)
        {
            disposed = true;
            if (!completed)
            {
                recorder.Rollback++;
            }

            recorder.TransactionDisposed++;
        }

        base.Dispose(disposing);
    }
}

namespace Smart.Data.Mocks;

using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

internal sealed class RecordingDbConnection : DbConnection
{
    private readonly Recorder recorder;

    private readonly bool failOnOpen;

    private ConnectionState state = ConnectionState.Closed;

    private bool disposed;

    private string connectionString = string.Empty;

    public RecordingDbConnection(Recorder recorder, bool failOnOpen)
    {
        this.recorder = recorder;
        this.failOnOpen = failOnOpen;
    }

    [AllowNull]
    public override string ConnectionString
    {
        get => connectionString;
        set => connectionString = value ?? string.Empty;
    }

    public override string Database => string.Empty;

    public override string DataSource => string.Empty;

    public override string ServerVersion => string.Empty;

    public override ConnectionState State => state;

    public override bool CanCreateBatch => true;

    public override void ChangeDatabase(string databaseName)
    {
    }

    public override void Close() => state = ConnectionState.Closed;

    public override void Open()
    {
        recorder.OpenSync++;
        if (failOnOpen)
        {
            throw new InvalidOperationException("Open failed.");
        }

        state = ConnectionState.Open;
    }

    public override Task OpenAsync(CancellationToken cancellationToken)
    {
        recorder.OpenAsync++;
        recorder.OpenToken = cancellationToken;
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        if (failOnOpen)
        {
            return Task.FromException(new InvalidOperationException("Open failed."));
        }

        state = ConnectionState.Open;
        return Task.CompletedTask;
    }

    public override ValueTask DisposeAsync()
    {
        MarkDisposed();
        return base.DisposeAsync();
    }

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
        recorder.BeginSync++;
        recorder.BeginLevel = isolationLevel;
        return new RecordingDbTransaction(this, recorder, isolationLevel);
    }

    protected override ValueTask<DbTransaction> BeginDbTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        recorder.BeginToken = cancellationToken;
        return base.BeginDbTransactionAsync(isolationLevel, cancellationToken);
    }

    protected override DbBatch CreateDbBatch()
    {
        recorder.BatchCreated++;
        return new RecordingDbBatch(this, recorder);
    }

    protected override DbCommand CreateDbCommand() => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            MarkDisposed();
        }

        state = ConnectionState.Closed;
        base.Dispose(disposing);
    }

    private void MarkDisposed()
    {
        if (!disposed)
        {
            disposed = true;
            recorder.ConnectionDisposed++;
        }
    }
}

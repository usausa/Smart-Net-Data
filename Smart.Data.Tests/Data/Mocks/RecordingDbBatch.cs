namespace Smart.Data.Mocks;

using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable ConvertToAutoProperty
// ReSharper disable ReplaceWithFieldKeyword
internal sealed class RecordingDbBatch : DbBatch
{
    private readonly Recorder recorder;

    private DbConnection? connection;

    private DbTransaction? transaction;

    private bool disposed;

    public RecordingDbBatch(DbConnection connection, Recorder recorder)
    {
        this.connection = connection;
        this.recorder = recorder;
    }

    public override int Timeout { get; set; }

    // Never accessed by the extension methods under test.
    protected override DbBatchCommandCollection DbBatchCommands => null!;

    protected override DbConnection? DbConnection
    {
        get => connection;
        set => connection = value;
    }

    protected override DbTransaction? DbTransaction
    {
        get => transaction;
        set
        {
            transaction = value;
            if (value is not null)
            {
                recorder.BatchTransactionSet++;
            }
        }
    }

    public override void Cancel() => throw new NotSupportedException();

    public override int ExecuteNonQuery() => throw new NotSupportedException();

    public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default) => throw new NotSupportedException();

    public override object ExecuteScalar() => throw new NotSupportedException();

    public override Task<object?> ExecuteScalarAsync(CancellationToken cancellationToken = default) => throw new NotSupportedException();

    public override void Prepare() => throw new NotSupportedException();

    public override Task PrepareAsync(CancellationToken cancellationToken = default) => throw new NotSupportedException();

    public override void Dispose()
    {
        if (!disposed)
        {
            disposed = true;
            recorder.BatchDisposed++;
        }

        base.Dispose();
    }

    protected override DbBatchCommand CreateDbBatchCommand() => throw new NotSupportedException();

    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) => throw new NotSupportedException();

    protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken) => throw new NotSupportedException();
}
// ReSharper restore ReplaceWithFieldKeyword
// ReSharper restore ConvertToAutoProperty

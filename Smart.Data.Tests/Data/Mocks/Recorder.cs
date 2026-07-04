namespace Smart.Data.Mocks;

using System.Data;
using System.Threading;

internal sealed class Recorder
{
    public int OpenSync { get; set; }

    public int OpenAsync { get; set; }

    public CancellationToken OpenToken { get; set; }

    public int ConnectionDisposed { get; set; }

    public int BeginSync { get; set; }

    public IsolationLevel BeginLevel { get; set; }

    public CancellationToken BeginToken { get; set; }

    public int Commit { get; set; }

    public CancellationToken CommitToken { get; set; }

    public int Rollback { get; set; }

    public int TransactionDisposed { get; set; }

    public int BatchCreated { get; set; }

    public int BatchTransactionSet { get; set; }

    public int BatchDisposed { get; set; }
}

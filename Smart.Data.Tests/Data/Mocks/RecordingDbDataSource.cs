namespace Smart.Data.Mocks;

using System.Data.Common;

internal sealed class RecordingDbDataSource : DbDataSource
{
    private readonly Recorder recorder;

    private readonly bool failOnOpen;

    public RecordingDbDataSource(Recorder recorder, bool failOnOpen = false)
    {
        this.recorder = recorder;
        this.failOnOpen = failOnOpen;
    }

    public override string ConnectionString => string.Empty;

    protected override DbConnection CreateDbConnection() => new RecordingDbConnection(recorder, failOnOpen);
}

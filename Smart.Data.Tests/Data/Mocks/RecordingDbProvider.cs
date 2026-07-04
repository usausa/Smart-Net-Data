namespace Smart.Data.Mocks;

using System.Data.Common;

internal sealed class RecordingDbProvider : IDbProvider
{
    private readonly Recorder recorder;

    private readonly bool failOnOpen;

    public RecordingDbProvider(Recorder recorder, bool failOnOpen = false)
    {
        this.recorder = recorder;
        this.failOnOpen = failOnOpen;
    }

    public DbConnection CreateConnection() => new RecordingDbConnection(recorder, failOnOpen);
}

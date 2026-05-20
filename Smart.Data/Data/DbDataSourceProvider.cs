namespace Smart.Data;

using System;
using System.Data.Common;
using System.Threading.Tasks;

public sealed class DbDataSourceProvider : IDbProvider, IDisposable, IAsyncDisposable
{
    private readonly DbDataSource dataSource;

    private readonly bool ownsDataSource;

    public DbDataSourceProvider(DbDataSource dataSource, bool ownsDataSource = false)
    {
        ArgumentNullException.ThrowIfNull(dataSource);
        this.dataSource = dataSource;
        this.ownsDataSource = ownsDataSource;
    }

    public DbConnection CreateConnection() => dataSource.CreateConnection();

    public void Dispose()
    {
        if (ownsDataSource)
        {
            dataSource.Dispose();
        }
    }

    public ValueTask DisposeAsync()
    {
        if (ownsDataSource)
        {
            return dataSource.DisposeAsync();
        }
        return ValueTask.CompletedTask;
    }
}

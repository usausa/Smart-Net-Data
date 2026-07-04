namespace Smart.Data;

using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

internal readonly struct DataSourceConnectionSource : IConnectionSource
{
    private readonly DbDataSource dataSource;

    public DataSourceConnectionSource(DbDataSource dataSource)
    {
        this.dataSource = dataSource;
    }

    public DbConnection Open() => dataSource.OpenConnection();

    public ValueTask<DbConnection> OpenAsync(CancellationToken cancellationToken) => dataSource.OpenConnectionAsync(cancellationToken);
}

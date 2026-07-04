namespace Smart.Data;

using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

internal interface IConnectionSource
{
    DbConnection Open();

    ValueTask<DbConnection> OpenAsync(CancellationToken cancellationToken);
}

namespace Smart.Data;

using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

internal readonly struct ProviderConnectionSource : IConnectionSource
{
    private readonly IDbProvider provider;

    public ProviderConnectionSource(IDbProvider provider)
    {
        this.provider = provider;
    }

    public DbConnection Open()
    {
        var con = provider.CreateConnection();
        var opened = false;
        try
        {
            con.Open();
            opened = true;
        }
        finally
        {
            if (!opened)
            {
                con.Dispose();
            }
        }

        return con;
    }

    public async ValueTask<DbConnection> OpenAsync(CancellationToken cancellationToken)
    {
        var con = provider.CreateConnection();
        var opened = false;
        try
        {
            await con.OpenAsync(cancellationToken).ConfigureAwait(false);
            opened = true;
        }
        finally
        {
            if (!opened)
            {
                await con.DisposeAsync().ConfigureAwait(false);
            }
        }

        return con;
    }
}

namespace Smart.Data;

using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CA1062
public static class DbConnectionExtensions
{
    public static void OpenIfNot(this IDbConnection con)
    {
        if ((con.State & ConnectionState.Open) != ConnectionState.Open)
        {
            con.Open();
        }
    }

    public static async ValueTask OpenIfNotAsync(this DbConnection con, CancellationToken cancellationToken = default)
    {
        if ((con.State & ConnectionState.Open) != ConnectionState.Open)
        {
            await con.OpenAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public static DbTransaction BeginTransactionWithOpen(this DbConnection con)
    {
        con.OpenIfNot();
        return con.BeginTransaction();
    }

    public static DbTransaction BeginTransactionWithOpen(this DbConnection con, IsolationLevel isolationLevel)
    {
        con.OpenIfNot();
        return con.BeginTransaction(isolationLevel);
    }

    public static async ValueTask<DbTransaction> BeginTransactionWithOpenAsync(this DbConnection con, CancellationToken cancellationToken = default)
    {
        await con.OpenIfNotAsync(cancellationToken).ConfigureAwait(false);
        return await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask<DbTransaction> BeginTransactionWithOpenAsync(this DbConnection con, IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
    {
        await con.OpenIfNotAsync(cancellationToken).ConfigureAwait(false);
        return await con.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);
    }
}
#pragma warning restore CA1062

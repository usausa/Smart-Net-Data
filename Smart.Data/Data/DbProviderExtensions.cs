namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

#pragma warning disable CA1062
public static class DbProviderExtensions
{
    // TODO with tx

    public static void Using(this IDbProvider factory, Action<DbConnection> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        action(con);
    }

    public static void Using<T>(this IDbProvider factory, T state, Action<DbConnection, T> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        action(con, state);
    }

    public static T Using<T>(this IDbProvider factory, Func<DbConnection, T> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        return func(con);
    }

    // TODO state

    public static IEnumerable<T> UsingDefer<T>(this IDbProvider factory, Func<DbConnection, IEnumerable<T>> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        foreach (var item in func(con))
        {
            yield return item;
        }
    }

    // TODO state

    public static async ValueTask UsingAsync(this IDbProvider factory, Func<DbConnection, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        await func(con).ConfigureAwait(false);
    }

    // TODO state

    public static async ValueTask<T> UsingAsync<T>(this IDbProvider factory, Func<DbConnection, ValueTask<T>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        return await func(con).ConfigureAwait(false);
    }

    // TODO state

    public static async IAsyncEnumerable<T> UsingDeferAsync<T>(this IDbProvider factory, Func<DbConnection, ValueTask<IEnumerable<T>>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        foreach (var item in await func(con).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    // TODO state

    public static async IAsyncEnumerable<T> UsingDeferAsync<T>(this IDbProvider factory, Func<DbConnection, IAsyncEnumerable<T>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        await foreach (var item in func(con).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    // TODO state

    public static void UsingTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        action(con, tx);
    }

    // TODO state

    public static T UsingTx<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, T> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        return func(con, tx);
    }

    // TODO state

    public static async ValueTask UsingTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
    }

    // TODO state

    public static async ValueTask<T> UsingTxAsync<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<T>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx).ConfigureAwait(false);
    }

    // TODO state

    public static async ValueTask UsingTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
    }

    // TODO state

    public static async ValueTask<T> UsingTxAsync<T>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<T>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx).ConfigureAwait(false);
    }

    // TODO state
}
#pragma warning restore CA1062

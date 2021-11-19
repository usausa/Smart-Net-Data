namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
public static class DbProviderExtensions
{
#if NETSTANDARD2_1
    public static void Using(this IDbProvider factory, Action<DbConnection> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        action(con);
    }

    public static T Using<T>(this IDbProvider factory, Func<DbConnection, T> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        return func(con);
    }

    public static IEnumerable<T> UsingDefer<T>(this IDbProvider factory, Func<DbConnection, IEnumerable<T>> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        foreach (var item in func(con))
        {
            yield return item;
        }
    }

    public static async ValueTask UsingAsync(this IDbProvider factory, Func<DbConnection, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        await func(con).ConfigureAwait(false);
    }

    public static async ValueTask<T> UsingAsync<T>(this IDbProvider factory, Func<DbConnection, ValueTask<T>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        return await func(con).ConfigureAwait(false);
    }

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

    public static void UsingTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        action(con, tx);
    }

    public static T UsingTx<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, T> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        return func(con, tx);
    }

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
#else
    public static void Using(this IDbProvider factory, Action<DbConnection> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        action(con);
    }

    public static T Using<T>(this IDbProvider factory, Func<DbConnection, T> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        return func(con);
    }

    public static IEnumerable<T> UsingDefer<T>(this IDbProvider factory, Func<DbConnection, IEnumerable<T>> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        foreach (var item in func(con))
        {
            yield return item;
        }
    }

    public static async Task UsingAsync(this IDbProvider factory, Func<DbConnection, Task> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        await func(con).ConfigureAwait(false);
    }

    public static async Task<T> UsingAsync<T>(this IDbProvider factory, Func<DbConnection, Task<T>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        return await func(con).ConfigureAwait(false);
    }

    public static void UsingTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        action(con, tx);
    }

    public static T UsingTx<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, T> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        return func(con, tx);
    }

    public static async Task UsingTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, Task> func)
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

    public static async Task<T> UsingTxAsync<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, Task<T>> func)
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

    public static async Task UsingTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, Task> func)
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

    public static async Task<T> UsingTxAsync<T>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, Task<T>> func)
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
#endif
}

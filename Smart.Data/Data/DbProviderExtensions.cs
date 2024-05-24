namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

#pragma warning disable CA1062
public static class DbProviderExtensions
{
    public static void Using(this IDbProvider factory, Action<DbConnection> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        action(con);
    }

    public static void Using<TState>(this IDbProvider factory, TState state, Action<DbConnection, TState> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        action(con, state);
    }

    public static TResult Using<TResult>(this IDbProvider factory, Func<DbConnection, TResult> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        return func(con);
    }

    public static TResult Using<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, TResult> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        return func(con, state);
    }

    public static IEnumerable<TResult> UsingDefer<TResult>(this IDbProvider factory, Func<DbConnection, IEnumerable<TResult>> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        foreach (var item in func(con))
        {
            yield return item;
        }
    }

    public static IEnumerable<TResult> UsingDefer<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, IEnumerable<TResult>> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        foreach (var item in func(con, state))
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

    public static async ValueTask UsingAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        await func(con, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAsync<TResult>(this IDbProvider factory, Func<DbConnection, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        return await func(con).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        return await func(con, state).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this IDbProvider factory, Func<DbConnection, ValueTask<IEnumerable<TResult>>> func)
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

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask<IEnumerable<TResult>>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        foreach (var item in await func(con, state).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this IDbProvider factory, Func<DbConnection, IAsyncEnumerable<TResult>> func)
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

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, IAsyncEnumerable<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
        await foreach (var item in func(con, state).ConfigureAwait(false))
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

    public static void UsingTx<TState>(this IDbProvider factory, TState state, Action<DbConnection, DbTransaction, TState> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        action(con, tx, state);
    }

    public static TResult UsingTx<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, TResult> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        return func(con, tx);
    }

    public static TResult UsingTx<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        return func(con, tx, state);
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

    public static async ValueTask UsingTxAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
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

    public static async ValueTask<TResult> UsingTxAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx, state).ConfigureAwait(false);
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

    public static async ValueTask UsingTxAsync<TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TResult>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
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

    public static async ValueTask<TResult> UsingTxAsync<TResult, TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx, state).ConfigureAwait(false);
    }

    public static void UsingAutoTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        action(con, tx);
        tx.Commit();
    }

    public static void UsingAutoTx<TState>(this IDbProvider factory, TState state, Action<DbConnection, DbTransaction, TState> action)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        action(con, tx, state);
        tx.Commit();
    }

    public static TResult UsingAutoTx<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, TResult> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        var result = func(con, tx);
        tx.Commit();
        return result;
    }

    public static TResult UsingAutoTx<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
    {
        using var con = factory.CreateConnection();
        con.Open();
        using var tx = con.BeginTransaction();
        var result = func(con, tx, state);
        tx.Commit();
        return result;
    }

    public static async ValueTask UsingAutoTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync().ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
        return result;
    }

    public static async ValueTask UsingAutoTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxAsync<TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
    {
#pragma warning disable CA2007
        await using var con = factory.CreateConnection();
#pragma warning restore CA2007
        await con.OpenAsync().ConfigureAwait(false);
#pragma warning disable CA2007
        await using var tx = await con.BeginTransactionAsync(level).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync().ConfigureAwait(false);
        return result;
    }
}
#pragma warning restore CA1062

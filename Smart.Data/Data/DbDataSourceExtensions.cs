namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CA1062
public static class DbDataSourceExtensions
{
    // ------------------------------------------------------------
    // Using
    // ------------------------------------------------------------

    public static void Using(this DbDataSource dataSource, Action<DbConnection> action)
    {
        using var con = dataSource.OpenConnection();
        action(con);
    }

    public static void Using<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, TState> action)
    {
        using var con = dataSource.OpenConnection();
        action(con, state);
    }

    public static TResult Using<TResult>(this DbDataSource dataSource, Func<DbConnection, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        return func(con);
    }

    public static TResult Using<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        return func(con, state);
    }

    public static IEnumerable<TResult> UsingDefer<TResult>(this DbDataSource dataSource, Func<DbConnection, IEnumerable<TResult>> func)
    {
        using var con = dataSource.OpenConnection();
        foreach (var item in func(con))
        {
            yield return item;
        }
    }

    public static IEnumerable<TResult> UsingDefer<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, IEnumerable<TResult>> func)
    {
        using var con = dataSource.OpenConnection();
        foreach (var item in func(con, state))
        {
            yield return item;
        }
    }

    public static async ValueTask UsingAsync(this DbDataSource dataSource, Func<DbConnection, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con).ConfigureAwait(false);
    }

    public static async ValueTask UsingAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, state).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, ValueTask<IEnumerable<TResult>>> func, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        foreach (var item in await func(con).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, ValueTask<IEnumerable<TResult>>> func, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        foreach (var item in await func(con, state).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, IAsyncEnumerable<TResult>> func, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await foreach (var item in func(con).WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, IAsyncEnumerable<TResult>> func, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await foreach (var item in func(con, state).WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    // ------------------------------------------------------------
    // Using + Transaction
    // ------------------------------------------------------------

    public static void UsingTx(this DbDataSource dataSource, Action<DbConnection, DbTransaction> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        action(con, tx);
    }

    public static void UsingTx<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, TState> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        action(con, tx, state);
    }

    public static TResult UsingTx<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        return func(con, tx);
    }

    public static TResult UsingTx<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        return func(con, tx, state);
    }

    public static async ValueTask UsingTxAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxAsync(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxAsync<TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TResult>(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TResult, TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx, state).ConfigureAwait(false);
    }

    // ------------------------------------------------------------
    // using + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTx(this DbDataSource dataSource, Action<DbConnection, DbTransaction> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        action(con, tx);
        tx.Commit();
    }

    public static void UsingAutoTx<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, TState> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        action(con, tx, state);
        tx.Commit();
    }

    public static TResult UsingAutoTx<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        var result = func(con, tx);
        tx.Commit();
        return result;
    }

    public static TResult UsingAutoTx<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        var result = func(con, tx, state);
        tx.Commit();
        return result;
    }

    public static async ValueTask UsingAutoTxAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask UsingAutoTxAsync(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxAsync<TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult>(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    // ------------------------------------------------------------
    // Batch
    // ------------------------------------------------------------

    public static void UsingBatch(this DbDataSource dataSource, Action<DbConnection, DbBatch> action)
    {
        using var con = dataSource.OpenConnection();
        using var batch = con.CreateBatch();
        action(con, batch);
    }

    public static void UsingBatch<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbBatch, TState> action)
    {
        using var con = dataSource.OpenConnection();
        using var batch = con.CreateBatch();
        action(con, batch, state);
    }

    public static TResult UsingBatch<TResult>(this DbDataSource dataSource, Func<DbConnection, DbBatch, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var batch = con.CreateBatch();
        return func(con, batch);
    }

    public static TResult UsingBatch<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbBatch, TState, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var batch = con.CreateBatch();
        return func(con, batch, state);
    }

    public static async ValueTask UsingBatchAsync(this DbDataSource dataSource, Func<DbConnection, DbBatch, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        await func(con, batch).ConfigureAwait(false);
    }

    public static async ValueTask UsingBatchAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        await func(con, batch, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingBatchAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        return await func(con, batch).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingBatchAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        return await func(con, batch, state).ConfigureAwait(false);
    }

    // ------------------------------------------------------------
    // Batch + Transaction
    // ------------------------------------------------------------

    public static void UsingTxBatch(this DbDataSource dataSource, Action<DbConnection, DbTransaction, DbBatch> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch);
    }

    public static void UsingTxBatch<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch, state);
    }

    public static TResult UsingTxBatch<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        return func(con, tx, batch);
    }

    public static TResult UsingTxBatch<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        return func(con, tx, batch, state);
    }

    public static async ValueTask UsingTxBatchAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxBatchAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxBatchAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        return await func(con, tx, batch).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxBatchAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        return await func(con, tx, batch, state).ConfigureAwait(false);
    }

    // ------------------------------------------------------------
    // Batch + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTxBatch(this DbDataSource dataSource, Action<DbConnection, DbTransaction, DbBatch> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch);
        tx.Commit();
    }

    public static void UsingAutoTxBatch<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch, state);
        tx.Commit();
    }

    public static TResult UsingAutoTxBatch<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        var result = func(con, tx, batch);
        tx.Commit();
        return result;
    }

    public static TResult UsingAutoTxBatch<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
    {
        using var con = dataSource.OpenConnection();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        var result = func(con, tx, batch, state);
        tx.Commit();
        return result;
    }

    public static async ValueTask UsingAutoTxBatchAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxBatchAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxBatchAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        var result = await func(con, tx, batch).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxBatchAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2007
        await using var con = await dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        var result = await func(con, tx, batch, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }
}
#pragma warning restore CA1062

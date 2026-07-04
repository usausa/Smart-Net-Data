namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

internal static class ConnectionOperations
{
    // ------------------------------------------------------------
    // Using
    // ------------------------------------------------------------

    public static void Using<TSource>(TSource source, Action<DbConnection> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        action(con);
    }

    public static void Using<TSource, TState>(TSource source, TState state, Action<DbConnection, TState> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        action(con, state);
    }

    public static TResult Using<TSource, TResult>(TSource source, Func<DbConnection, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        return func(con);
    }

    public static TResult Using<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, TState, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        return func(con, state);
    }

    public static IEnumerable<TResult> UsingDefer<TSource, TResult>(TSource source, Func<DbConnection, IEnumerable<TResult>> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        foreach (var item in func(con))
        {
            yield return item;
        }
    }

    public static IEnumerable<TResult> UsingDefer<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, TState, IEnumerable<TResult>> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        foreach (var item in func(con, state))
        {
            yield return item;
        }
    }

    public static async ValueTask UsingAsync<TSource>(TSource source, Func<DbConnection, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con).ConfigureAwait(false);
    }

    public static async ValueTask UsingAsync<TSource, TState>(TSource source, TState state, Func<DbConnection, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAsync<TSource, TResult>(TSource source, Func<DbConnection, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, state).ConfigureAwait(false);
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TSource, TResult>(TSource source, Func<DbConnection, ValueTask<IEnumerable<TResult>>> func, [EnumeratorCancellation] CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        foreach (var item in await func(con).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, TState, ValueTask<IEnumerable<TResult>>> func, [EnumeratorCancellation] CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        foreach (var item in await func(con, state).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TSource, TResult>(TSource source, Func<DbConnection, IAsyncEnumerable<TResult>> func, [EnumeratorCancellation] CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await foreach (var item in func(con).WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    public static async IAsyncEnumerable<TResult> UsingDeferAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, TState, IAsyncEnumerable<TResult>> func, [EnumeratorCancellation] CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await foreach (var item in func(con, state).WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return item;
        }
    }

    // ------------------------------------------------------------
    // Using + Transaction
    // ------------------------------------------------------------

    public static void UsingTx<TSource>(TSource source, Action<DbConnection, DbTransaction> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        action(con, tx);
    }

    public static void UsingTx<TSource, TState>(TSource source, TState state, Action<DbConnection, DbTransaction, TState> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        action(con, tx, state);
    }

    public static TResult UsingTx<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        return func(con, tx);
    }

    public static TResult UsingTx<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        return func(con, tx, state);
    }

    public static async ValueTask UsingTxAsync<TSource>(TSource source, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxAsync<TSource, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxAsync<TSource>(TSource source, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxAsync<TSource, TState>(TSource source, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TSource, TResult>(TSource source, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxAsync<TSource, TResult, TState>(TSource source, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        return await func(con, tx, state).ConfigureAwait(false);
    }

    // ------------------------------------------------------------
    // Using + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTx<TSource>(TSource source, Action<DbConnection, DbTransaction> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        action(con, tx);
        tx.Commit();
    }

    public static void UsingAutoTx<TSource, TState>(TSource source, TState state, Action<DbConnection, DbTransaction, TState> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        action(con, tx, state);
        tx.Commit();
    }

    public static TResult UsingAutoTx<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        var result = func(con, tx);
        tx.Commit();
        return result;
    }

    public static TResult UsingAutoTx<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        var result = func(con, tx, state);
        tx.Commit();
        return result;
    }

    public static async ValueTask UsingAutoTxAsync<TSource>(TSource source, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxAsync<TSource, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask UsingAutoTxAsync<TSource>(TSource source, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxAsync<TSource, TState>(TSource source, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TSource, TResult>(TSource source, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxAsync<TSource, TResult, TState>(TSource source, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(level, cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007
        var result = await func(con, tx, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    // ------------------------------------------------------------
    // Batch
    // ------------------------------------------------------------

    public static void UsingBatch<TSource>(TSource source, Action<DbConnection, DbBatch> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var batch = con.CreateBatch();
        action(con, batch);
    }

    public static void UsingBatch<TSource, TState>(TSource source, TState state, Action<DbConnection, DbBatch, TState> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var batch = con.CreateBatch();
        action(con, batch, state);
    }

    public static TResult UsingBatch<TSource, TResult>(TSource source, Func<DbConnection, DbBatch, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var batch = con.CreateBatch();
        return func(con, batch);
    }

    public static TResult UsingBatch<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbBatch, TState, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var batch = con.CreateBatch();
        return func(con, batch, state);
    }

    public static async ValueTask UsingBatchAsync<TSource>(TSource source, Func<DbConnection, DbBatch, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        await func(con, batch).ConfigureAwait(false);
    }

    public static async ValueTask UsingBatchAsync<TSource, TState>(TSource source, TState state, Func<DbConnection, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        await func(con, batch, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingBatchAsync<TSource, TResult>(TSource source, Func<DbConnection, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        return await func(con, batch).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingBatchAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        return await func(con, batch, state).ConfigureAwait(false);
    }

    // ------------------------------------------------------------
    // Batch + Transaction
    // ------------------------------------------------------------

    public static void UsingTxBatch<TSource>(TSource source, Action<DbConnection, DbTransaction, DbBatch> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch);
    }

    public static void UsingTxBatch<TSource, TState>(TSource source, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch, state);
    }

    public static TResult UsingTxBatch<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        return func(con, tx, batch);
    }

    public static TResult UsingTxBatch<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        return func(con, tx, batch, state);
    }

    public static async ValueTask UsingTxBatchAsync<TSource>(TSource source, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch).ConfigureAwait(false);
    }

    public static async ValueTask UsingTxBatchAsync<TSource, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch, state).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxBatchAsync<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        return await func(con, tx, batch).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingTxBatchAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        return await func(con, tx, batch, state).ConfigureAwait(false);
    }

    // ------------------------------------------------------------
    // Batch + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTxBatch<TSource>(TSource source, Action<DbConnection, DbTransaction, DbBatch> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch);
        tx.Commit();
    }

    public static void UsingAutoTxBatch<TSource, TState>(TSource source, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        action(con, tx, batch, state);
        tx.Commit();
    }

    public static TResult UsingAutoTxBatch<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        var result = func(con, tx, batch);
        tx.Commit();
        return result;
    }

    public static TResult UsingAutoTxBatch<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
        where TSource : IConnectionSource
    {
        using var con = source.Open();
        using var tx = con.BeginTransaction();
        using var batch = con.CreateBatch();
        batch.Transaction = tx;
        var result = func(con, tx, batch, state);
        tx.Commit();
        return result;
    }

    public static async ValueTask UsingAutoTxBatchAsync<TSource>(TSource source, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask UsingAutoTxBatchAsync<TSource, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        await func(con, tx, batch, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask<TResult> UsingAutoTxBatchAsync<TSource, TResult>(TSource source, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        var result = await func(con, tx, batch).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<TResult> UsingAutoTxBatchAsync<TSource, TResult, TState>(TSource source, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        where TSource : IConnectionSource
    {
#pragma warning disable CA2007
        await using var con = await source.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await con.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        await using var batch = con.CreateBatch();
#pragma warning restore CA2007
        batch.Transaction = tx;
        var result = await func(con, tx, batch, state).ConfigureAwait(false);
        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }
}

namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

public static class DbDataSourceExtensions
{
    // ------------------------------------------------------------
    // Using
    // ------------------------------------------------------------

    public static void Using(this DbDataSource dataSource, Action<DbConnection> action)
        => ConnectionOperations.Using(new DataSourceConnectionSource(dataSource), action);

    public static void Using<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, TState> action)
        => ConnectionOperations.Using(new DataSourceConnectionSource(dataSource), state, action);

    public static TResult Using<TResult>(this DbDataSource dataSource, Func<DbConnection, TResult> func)
        => ConnectionOperations.Using(new DataSourceConnectionSource(dataSource), func);

    public static TResult Using<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, TResult> func)
        => ConnectionOperations.Using(new DataSourceConnectionSource(dataSource), state, func);

    public static IEnumerable<TResult> UsingDefer<TResult>(this DbDataSource dataSource, Func<DbConnection, IEnumerable<TResult>> func)
        => ConnectionOperations.UsingDefer(new DataSourceConnectionSource(dataSource), func);

    public static IEnumerable<TResult> UsingDefer<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, IEnumerable<TResult>> func)
        => ConnectionOperations.UsingDefer(new DataSourceConnectionSource(dataSource), state, func);

    public static ValueTask UsingAsync(this DbDataSource dataSource, Func<DbConnection, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask UsingAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask<TResult> UsingAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask<TResult> UsingAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, ValueTask<IEnumerable<TResult>>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingDeferAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, ValueTask<IEnumerable<TResult>>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingDeferAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, IAsyncEnumerable<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingDeferAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, TState, IAsyncEnumerable<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingDeferAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    // ------------------------------------------------------------
    // Using + Transaction
    // ------------------------------------------------------------

    public static void UsingTx(this DbDataSource dataSource, Action<DbConnection, DbTransaction> action)
        => ConnectionOperations.UsingTx(new DataSourceConnectionSource(dataSource), action);

    public static void UsingTx<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, TState> action)
        => ConnectionOperations.UsingTx(new DataSourceConnectionSource(dataSource), state, action);

    public static TResult UsingTx<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, TResult> func)
        => ConnectionOperations.UsingTx(new DataSourceConnectionSource(dataSource), func);

    public static TResult UsingTx<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
        => ConnectionOperations.UsingTx(new DataSourceConnectionSource(dataSource), state, func);

    public static ValueTask UsingTxAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask UsingTxAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask UsingTxAsync(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), level, func, cancellationToken);

    public static ValueTask UsingTxAsync<TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), level, state, func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult>(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), level, func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult, TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxAsync(new DataSourceConnectionSource(dataSource), level, state, func, cancellationToken);

    // ------------------------------------------------------------
    // using + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTx(this DbDataSource dataSource, Action<DbConnection, DbTransaction> action)
        => ConnectionOperations.UsingAutoTx(new DataSourceConnectionSource(dataSource), action);

    public static void UsingAutoTx<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, TState> action)
        => ConnectionOperations.UsingAutoTx(new DataSourceConnectionSource(dataSource), state, action);

    public static TResult UsingAutoTx<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, TResult> func)
        => ConnectionOperations.UsingAutoTx(new DataSourceConnectionSource(dataSource), func);

    public static TResult UsingAutoTx<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
        => ConnectionOperations.UsingAutoTx(new DataSourceConnectionSource(dataSource), state, func);

    public static ValueTask UsingAutoTxAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask UsingAutoTxAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask UsingAutoTxAsync(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), level, func, cancellationToken);

    public static ValueTask UsingAutoTxAsync<TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), level, state, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult>(this DbDataSource dataSource, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), level, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this DbDataSource dataSource, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxAsync(new DataSourceConnectionSource(dataSource), level, state, func, cancellationToken);

    // ------------------------------------------------------------
    // Batch
    // ------------------------------------------------------------

    public static void UsingBatch(this DbDataSource dataSource, Action<DbConnection, DbBatch> action)
        => ConnectionOperations.UsingBatch(new DataSourceConnectionSource(dataSource), action);

    public static void UsingBatch<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbBatch, TState> action)
        => ConnectionOperations.UsingBatch(new DataSourceConnectionSource(dataSource), state, action);

    public static TResult UsingBatch<TResult>(this DbDataSource dataSource, Func<DbConnection, DbBatch, TResult> func)
        => ConnectionOperations.UsingBatch(new DataSourceConnectionSource(dataSource), func);

    public static TResult UsingBatch<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbBatch, TState, TResult> func)
        => ConnectionOperations.UsingBatch(new DataSourceConnectionSource(dataSource), state, func);

    public static ValueTask UsingBatchAsync(this DbDataSource dataSource, Func<DbConnection, DbBatch, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingBatchAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask UsingBatchAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingBatchAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask<TResult> UsingBatchAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingBatchAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask<TResult> UsingBatchAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingBatchAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    // ------------------------------------------------------------
    // Batch + Transaction
    // ------------------------------------------------------------

    public static void UsingTxBatch(this DbDataSource dataSource, Action<DbConnection, DbTransaction, DbBatch> action)
        => ConnectionOperations.UsingTxBatch(new DataSourceConnectionSource(dataSource), action);

    public static void UsingTxBatch<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
        => ConnectionOperations.UsingTxBatch(new DataSourceConnectionSource(dataSource), state, action);

    public static TResult UsingTxBatch<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
        => ConnectionOperations.UsingTxBatch(new DataSourceConnectionSource(dataSource), func);

    public static TResult UsingTxBatch<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
        => ConnectionOperations.UsingTxBatch(new DataSourceConnectionSource(dataSource), state, func);

    public static ValueTask UsingTxBatchAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxBatchAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask UsingTxBatchAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxBatchAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask<TResult> UsingTxBatchAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxBatchAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask<TResult> UsingTxBatchAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingTxBatchAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    // ------------------------------------------------------------
    // Batch + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTxBatch(this DbDataSource dataSource, Action<DbConnection, DbTransaction, DbBatch> action)
        => ConnectionOperations.UsingAutoTxBatch(new DataSourceConnectionSource(dataSource), action);

    public static void UsingAutoTxBatch<TState>(this DbDataSource dataSource, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
        => ConnectionOperations.UsingAutoTxBatch(new DataSourceConnectionSource(dataSource), state, action);

    public static TResult UsingAutoTxBatch<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
        => ConnectionOperations.UsingAutoTxBatch(new DataSourceConnectionSource(dataSource), func);

    public static TResult UsingAutoTxBatch<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
        => ConnectionOperations.UsingAutoTxBatch(new DataSourceConnectionSource(dataSource), state, func);

    public static ValueTask UsingAutoTxBatchAsync(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxBatchAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask UsingAutoTxBatchAsync<TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxBatchAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxBatchAsync<TResult>(this DbDataSource dataSource, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxBatchAsync(new DataSourceConnectionSource(dataSource), func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxBatchAsync<TResult, TState>(this DbDataSource dataSource, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken = default)
        => ConnectionOperations.UsingAutoTxBatchAsync(new DataSourceConnectionSource(dataSource), state, func, cancellationToken);
}

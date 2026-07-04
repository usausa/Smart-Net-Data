namespace Smart.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

public static class DbProviderExtensions
{
    // ------------------------------------------------------------
    // Using
    // ------------------------------------------------------------

    public static void Using(this IDbProvider factory, Action<DbConnection> action)
        => ConnectionOperations.Using(new ProviderConnectionSource(factory), action);

    public static void Using<TState>(this IDbProvider factory, TState state, Action<DbConnection, TState> action)
        => ConnectionOperations.Using(new ProviderConnectionSource(factory), state, action);

    public static TResult Using<TResult>(this IDbProvider factory, Func<DbConnection, TResult> func)
        => ConnectionOperations.Using(new ProviderConnectionSource(factory), func);

    public static TResult Using<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, TResult> func)
        => ConnectionOperations.Using(new ProviderConnectionSource(factory), state, func);

    public static IEnumerable<TResult> UsingDefer<TResult>(this IDbProvider factory, Func<DbConnection, IEnumerable<TResult>> func)
        => ConnectionOperations.UsingDefer(new ProviderConnectionSource(factory), func);

    public static IEnumerable<TResult> UsingDefer<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, IEnumerable<TResult>> func)
        => ConnectionOperations.UsingDefer(new ProviderConnectionSource(factory), state, func);

    public static ValueTask UsingAsync(this IDbProvider factory, Func<DbConnection, ValueTask> func)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask UsingAsync(this IDbProvider factory, Func<DbConnection, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask UsingAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask> func)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask UsingAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask<TResult> UsingAsync<TResult>(this IDbProvider factory, Func<DbConnection, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask<TResult> UsingAsync<TResult>(this IDbProvider factory, Func<DbConnection, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask<TResult> UsingAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask<TResult> UsingAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this IDbProvider factory, Func<DbConnection, ValueTask<IEnumerable<TResult>>> func)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), func, default);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this IDbProvider factory, Func<DbConnection, ValueTask<IEnumerable<TResult>>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask<IEnumerable<TResult>>> func)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), state, func, default);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, ValueTask<IEnumerable<TResult>>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this IDbProvider factory, Func<DbConnection, IAsyncEnumerable<TResult>> func)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), func, default);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult>(this IDbProvider factory, Func<DbConnection, IAsyncEnumerable<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, IAsyncEnumerable<TResult>> func)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), state, func, default);

    public static IAsyncEnumerable<TResult> UsingDeferAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, TState, IAsyncEnumerable<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingDeferAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    // ------------------------------------------------------------
    // Using + Transaction
    // ------------------------------------------------------------

    public static void UsingTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
        => ConnectionOperations.UsingTx(new ProviderConnectionSource(factory), action);

    public static void UsingTx<TState>(this IDbProvider factory, TState state, Action<DbConnection, DbTransaction, TState> action)
        => ConnectionOperations.UsingTx(new ProviderConnectionSource(factory), state, action);

    public static TResult UsingTx<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, TResult> func)
        => ConnectionOperations.UsingTx(new ProviderConnectionSource(factory), func);

    public static TResult UsingTx<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
        => ConnectionOperations.UsingTx(new ProviderConnectionSource(factory), state, func);

    public static ValueTask UsingTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask UsingTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask UsingTxAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask UsingTxAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask<TResult> UsingTxAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask<TResult> UsingTxAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask UsingTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, func, default);

    public static ValueTask UsingTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, func, cancellationToken);

    public static ValueTask UsingTxAsync<TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, state, func, default);

    public static ValueTask UsingTxAsync<TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, state, func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, func, default);

    public static ValueTask<TResult> UsingTxAsync<TResult>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, func, cancellationToken);

    public static ValueTask<TResult> UsingTxAsync<TResult, TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, state, func, default);

    public static ValueTask<TResult> UsingTxAsync<TResult, TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxAsync(new ProviderConnectionSource(factory), level, state, func, cancellationToken);

    // ------------------------------------------------------------
    // Using + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
        => ConnectionOperations.UsingAutoTx(new ProviderConnectionSource(factory), action);

    public static void UsingAutoTx<TState>(this IDbProvider factory, TState state, Action<DbConnection, DbTransaction, TState> action)
        => ConnectionOperations.UsingAutoTx(new ProviderConnectionSource(factory), state, action);

    public static TResult UsingAutoTx<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, TResult> func)
        => ConnectionOperations.UsingAutoTx(new ProviderConnectionSource(factory), func);

    public static TResult UsingAutoTx<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, TResult> func)
        => ConnectionOperations.UsingAutoTx(new ProviderConnectionSource(factory), state, func);

    public static ValueTask UsingAutoTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask UsingAutoTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask UsingAutoTxAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask UsingAutoTxAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask UsingAutoTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, func, default);

    public static ValueTask UsingAutoTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, func, cancellationToken);

    public static ValueTask UsingAutoTxAsync<TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, state, func, default);

    public static ValueTask UsingAutoTxAsync<TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, state, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, func, default);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, state, func, default);

    public static ValueTask<TResult> UsingAutoTxAsync<TResult, TState>(this IDbProvider factory, IsolationLevel level, TState state, Func<DbConnection, DbTransaction, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxAsync(new ProviderConnectionSource(factory), level, state, func, cancellationToken);

    // ------------------------------------------------------------
    // Batch
    // ------------------------------------------------------------

    public static void UsingBatch(this IDbProvider factory, Action<DbConnection, DbBatch> action)
        => ConnectionOperations.UsingBatch(new ProviderConnectionSource(factory), action);

    public static void UsingBatch<TState>(this IDbProvider factory, TState state, Action<DbConnection, DbBatch, TState> action)
        => ConnectionOperations.UsingBatch(new ProviderConnectionSource(factory), state, action);

    public static TResult UsingBatch<TResult>(this IDbProvider factory, Func<DbConnection, DbBatch, TResult> func)
        => ConnectionOperations.UsingBatch(new ProviderConnectionSource(factory), func);

    public static TResult UsingBatch<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbBatch, TState, TResult> func)
        => ConnectionOperations.UsingBatch(new ProviderConnectionSource(factory), state, func);

    public static ValueTask UsingBatchAsync(this IDbProvider factory, Func<DbConnection, DbBatch, ValueTask> func)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask UsingBatchAsync(this IDbProvider factory, Func<DbConnection, DbBatch, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask UsingBatchAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbBatch, TState, ValueTask> func)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask UsingBatchAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask<TResult> UsingBatchAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbBatch, ValueTask<TResult>> func)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask<TResult> UsingBatchAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask<TResult> UsingBatchAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbBatch, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask<TResult> UsingBatchAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingBatchAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    // ------------------------------------------------------------
    // Batch + Transaction
    // ------------------------------------------------------------

    public static void UsingTxBatch(this IDbProvider factory, Action<DbConnection, DbTransaction, DbBatch> action)
        => ConnectionOperations.UsingTxBatch(new ProviderConnectionSource(factory), action);

    public static void UsingTxBatch<TState>(this IDbProvider factory, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
        => ConnectionOperations.UsingTxBatch(new ProviderConnectionSource(factory), state, action);

    public static TResult UsingTxBatch<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
        => ConnectionOperations.UsingTxBatch(new ProviderConnectionSource(factory), func);

    public static TResult UsingTxBatch<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
        => ConnectionOperations.UsingTxBatch(new ProviderConnectionSource(factory), state, func);

    public static ValueTask UsingTxBatchAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask UsingTxBatchAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask UsingTxBatchAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask UsingTxBatchAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask<TResult> UsingTxBatchAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask<TResult> UsingTxBatchAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask<TResult> UsingTxBatchAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask<TResult> UsingTxBatchAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingTxBatchAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    // ------------------------------------------------------------
    // Batch + Auto-commit Transaction
    // ------------------------------------------------------------

    public static void UsingAutoTxBatch(this IDbProvider factory, Action<DbConnection, DbTransaction, DbBatch> action)
        => ConnectionOperations.UsingAutoTxBatch(new ProviderConnectionSource(factory), action);

    public static void UsingAutoTxBatch<TState>(this IDbProvider factory, TState state, Action<DbConnection, DbTransaction, DbBatch, TState> action)
        => ConnectionOperations.UsingAutoTxBatch(new ProviderConnectionSource(factory), state, action);

    public static TResult UsingAutoTxBatch<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, TResult> func)
        => ConnectionOperations.UsingAutoTxBatch(new ProviderConnectionSource(factory), func);

    public static TResult UsingAutoTxBatch<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, TResult> func)
        => ConnectionOperations.UsingAutoTxBatch(new ProviderConnectionSource(factory), state, func);

    public static ValueTask UsingAutoTxBatchAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask UsingAutoTxBatchAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask UsingAutoTxBatchAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask UsingAutoTxBatchAsync<TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxBatchAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), func, default);

    public static ValueTask<TResult> UsingAutoTxBatchAsync<TResult>(this IDbProvider factory, Func<DbConnection, DbTransaction, DbBatch, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), func, cancellationToken);

    public static ValueTask<TResult> UsingAutoTxBatchAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), state, func, default);

    public static ValueTask<TResult> UsingAutoTxBatchAsync<TResult, TState>(this IDbProvider factory, TState state, Func<DbConnection, DbTransaction, DbBatch, TState, ValueTask<TResult>> func, CancellationToken cancellationToken)
        => ConnectionOperations.UsingAutoTxBatchAsync(new ProviderConnectionSource(factory), state, func, cancellationToken);
}

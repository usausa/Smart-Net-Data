namespace Smart.Data
{
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
            using (var con = factory.CreateConnection())
            {
                con.Open();
                action(con);
            }
        }

        public static T Using<T>(this IDbProvider factory, Func<DbConnection, T> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                return func(con);
            }
        }

        public static IEnumerable<T> UsingDefer<T>(this IDbProvider factory, Func<DbConnection, IEnumerable<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                foreach (var item in func(con))
                {
                    yield return item;
                }
            }
        }

        public static async Task UsingAsync(this IDbProvider factory, Func<DbConnection, Task> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await func(con).ConfigureAwait(false);
            }
        }

        public static async Task<T> UsingAsync<T>(this IDbProvider factory, Func<DbConnection, Task<T>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                return await func(con).ConfigureAwait(false);
            }
        }

        public static async ValueTask UsingAsync(this IDbProvider factory, Func<DbConnection, ValueTask> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await func(con).ConfigureAwait(false);
            }
        }

        public static async ValueTask<T> UsingAsync<T>(this IDbProvider factory, Func<DbConnection, ValueTask<T>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                return await func(con).ConfigureAwait(false);
            }
        }

        public static async IAsyncEnumerable<T> UsingDeferAsync<T>(this IDbProvider factory, Func<DbConnection, Task<IEnumerable<T>>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                foreach (var item in await func(con).ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }

        public static async IAsyncEnumerable<T> UsingDeferAsync<T>(this IDbProvider factory, Func<DbConnection, ValueTask<IEnumerable<T>>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                foreach (var item in await func(con).ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }

        public static async IAsyncEnumerable<T> UsingDeferAsync<T>(this IDbProvider factory, Func<DbConnection, IAsyncEnumerable<T>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await foreach (var item in func(con).ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }

        public static void UsingTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    action(con, tx);
                }
            }
        }

        public static T UsingTx<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, T> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    return func(con, tx);
                }
            }
        }

        public static async Task UsingTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, Task> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction())
                {
                    await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async Task<T> UsingTxAsync<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, Task<T>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction())
                {
                    return await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async Task UsingTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, Task> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction(level))
                {
                    await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async Task<T> UsingTxAsync<T>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, Task<T>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction(level))
                {
                    return await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async ValueTask UsingTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction())
                {
                    await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async ValueTask<T> UsingTxAsync<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, ValueTask<T>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction())
                {
                    return await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async ValueTask UsingTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction(level))
                {
                    await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async ValueTask<T> UsingTxAsync<T>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, ValueTask<T>> func)
        {
            await using (var con = factory.CreateConnection())
            {
                con.Open();
                await using (var tx = con.BeginTransaction(level))
                {
                    return await func(con, tx).ConfigureAwait(false);
                }
            }
        }
#else
        public static void Using(this IDbProvider factory, Action<DbConnection> action)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                action(con);
            }
        }

        public static T Using<T>(this IDbProvider factory, Func<DbConnection, T> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                return func(con);
            }
        }

        public static IEnumerable<T> UsingDefer<T>(this IDbProvider factory, Func<DbConnection, IEnumerable<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                foreach (var item in func(con))
                {
                    yield return item;
                }
            }
        }

        public static async Task UsingAsync(this IDbProvider factory, Func<DbConnection, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                await func(con).ConfigureAwait(false);
            }
        }

        public static async Task<T> UsingAsync<T>(this IDbProvider factory, Func<DbConnection, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                return await func(con).ConfigureAwait(false);
            }
        }

        public static void UsingTx(this IDbProvider factory, Action<DbConnection, DbTransaction> action)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    action(con, tx);
                }
            }
        }

        public static T UsingTx<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, T> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    return func(con, tx);
                }
            }
        }

        public static async Task UsingTxAsync(this IDbProvider factory, Func<DbConnection, DbTransaction, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async Task<T> UsingTxAsync<T>(this IDbProvider factory, Func<DbConnection, DbTransaction, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    return await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async Task UsingTxAsync(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction(level))
                {
                    await func(con, tx).ConfigureAwait(false);
                }
            }
        }

        public static async Task<T> UsingTxAsync<T>(this IDbProvider factory, IsolationLevel level, Func<DbConnection, DbTransaction, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction(level))
                {
                    return await func(con, tx).ConfigureAwait(false);
                }
            }
        }
#endif
    }
}

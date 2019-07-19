namespace Smart.Data
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public static class DbProviderExtensions
    {
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
    }
}

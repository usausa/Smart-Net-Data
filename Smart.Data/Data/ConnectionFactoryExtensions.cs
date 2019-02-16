namespace Smart.Data
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public static class ConnectionFactoryExtensions
    {
        public static void Using(this IConnectionFactory factory, Action<IDbConnection> action)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                action(con);
            }
        }

        public static T Using<T>(this IConnectionFactory factory, Func<IDbConnection, T> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                return func(con);
            }
        }

        public static async Task UsingAsync(this IConnectionFactory factory, Func<IDbConnection, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                await func(con);
            }
        }

        public static async Task<T> UsingAsync<T>(this IConnectionFactory factory, Func<IDbConnection, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                return await func(con);
            }
        }

        public static void UsingTx(this IConnectionFactory factory, Action<IDbConnection, IDbTransaction> action)
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

        public static T UsingTx<T>(this IConnectionFactory factory, Func<IDbConnection, IDbTransaction, T> func)
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

        public static async Task UsingTxAsync(this IConnectionFactory factory, Func<IDbConnection, IDbTransaction, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    await func(con, tx);
                }
            }
        }

        public static async Task<T> UsingTxAsync<T>(this IConnectionFactory factory, Func<IDbConnection, IDbTransaction, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    return await func(con, tx);
                }
            }
        }

        public static async Task UsingTxAsync(this IConnectionFactory factory, IsolationLevel level, Func<IDbConnection, IDbTransaction, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction(level))
                {
                    await func(con, tx);
                }
            }
        }

        public static async Task<T> UsingTxAsync<T>(this IConnectionFactory factory, IsolationLevel level, Func<IDbConnection, IDbTransaction, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction(level))
                {
                    return await func(con, tx);
                }
            }
        }
    }
}

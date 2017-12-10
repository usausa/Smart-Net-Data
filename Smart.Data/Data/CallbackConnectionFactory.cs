namespace Smart.Data
{
    using System;
    using System.Data;

    /// <summary>
    ///
    /// </summary>
    public sealed class CallbackConnectionFactory : IConnectionFactory
    {
        private readonly Func<IDbConnection> factory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        public CallbackConnectionFactory(Func<IDbConnection> factory)
        {
            this.factory = factory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return factory();
        }
    }
}

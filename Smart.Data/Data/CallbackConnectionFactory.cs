namespace Smart.Data
{
    using System;
    using System.Data;

    public sealed class CallbackConnectionFactory : IConnectionFactory
    {
        private readonly Func<IDbConnection> factory;

        public CallbackConnectionFactory(Func<IDbConnection> factory)
        {
            this.factory = factory;
        }

        public IDbConnection CreateConnection()
        {
            return factory();
        }
    }
}

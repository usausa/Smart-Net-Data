namespace Smart.Data
{
    using System.Data.Common;

    public sealed class DbProviderAdaptoer
    {
        private readonly DbProviderFactory factory;

        private readonly string connectionString;

        public DbProviderAdaptoer(DbProviderFactory factory, string connectionString)
        {
            this.factory = factory;
            this.connectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            var con = factory.CreateConnection();
            con.ConnectionString = connectionString;
            return con;
        }
    }
}

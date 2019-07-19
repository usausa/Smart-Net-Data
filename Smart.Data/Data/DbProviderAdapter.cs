namespace Smart.Data
{
    using System.Data.Common;

    public sealed class DbProviderAdapter
    {
        private readonly DbProviderFactory factory;

        private readonly string connectionString;

        public DbProviderAdapter(DbProviderFactory factory, string connectionString)
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

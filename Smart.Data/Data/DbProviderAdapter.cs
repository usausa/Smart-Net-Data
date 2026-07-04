namespace Smart.Data;

using System;
using System.Data.Common;

public sealed class DbProviderAdapter : IDbProvider
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
        if (con is null)
        {
            throw new InvalidOperationException("DbProviderFactory returned a null connection.");
        }

        con.ConnectionString = connectionString;
        return con;
    }
}

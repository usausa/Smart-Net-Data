namespace Smart.Data;

using System;
using System.Data.Common;

public sealed class DelegateDbProvider : IDbProvider
{
    private readonly Func<DbConnection> factory;

    public DelegateDbProvider(Func<DbConnection> factory)
    {
        this.factory = factory;
    }

    public DbConnection CreateConnection() => factory();
}

namespace Smart.Data
{
    using System.Data.Common;

    public interface IDbProvider
    {
        DbConnection CreateConnection();
    }
}

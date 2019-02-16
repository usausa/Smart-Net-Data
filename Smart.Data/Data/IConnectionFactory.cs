namespace Smart.Data
{
    using System.Data;

    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

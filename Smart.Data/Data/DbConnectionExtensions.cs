namespace Smart.Data
{
    using System.Data;

    public static class DbConnectionExtensions
    {
        public static void OpenIfNot(this IDbConnection con)
        {
            if ((con.State & ConnectionState.Open) != ConnectionState.Open)
            {
                con.Open();
            }
        }
    }
}

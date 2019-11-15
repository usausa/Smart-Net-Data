namespace Smart.Data
{
    using System.Data;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
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

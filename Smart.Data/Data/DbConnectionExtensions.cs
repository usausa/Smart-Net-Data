namespace Smart.Data;

using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

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

    public static async ValueTask OpenIfNotAsync(this DbConnection con)
    {
        if ((con.State & ConnectionState.Open) != ConnectionState.Open)
        {
            await con.OpenAsync().ConfigureAwait(false);
        }
    }
}

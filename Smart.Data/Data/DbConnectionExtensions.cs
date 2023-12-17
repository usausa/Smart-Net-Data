namespace Smart.Data;

using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

#pragma warning disable CA1062
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
#pragma warning restore CA1062

namespace Smart.Data.SqlClient;

using System.Data.Common;
using System.Text.RegularExpressions;

using Microsoft.Data.SqlClient;

public sealed partial class SqlDialect : IDialect
{
    [GeneratedRegex(@"[%_\[]")]
    private static partial Regex LikeEscapeRegex();

    public bool IsDuplicate(DbException ex)
    {
        return ex is SqlException { Number: 2627 or 2601 };
    }

    public string LikeEscape(string value)
    {
        return LikeEscapeRegex().Replace(value, "[$0]");
    }
}

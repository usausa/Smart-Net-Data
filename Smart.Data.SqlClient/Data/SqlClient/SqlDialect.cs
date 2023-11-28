namespace Smart.Data.SqlClient;

using System.Data.Common;
using System.Text.RegularExpressions;

using Microsoft.Data.SqlClient;

public sealed class SqlDialect : IDialect
{
    public bool IsDuplicate(DbException ex)
    {
        return ex is SqlException { Number: 2627 };
    }

    public string LikeEscape(string value)
    {
        return Regex.Replace(value, @"[%_\[]", "[$0]");
    }
}

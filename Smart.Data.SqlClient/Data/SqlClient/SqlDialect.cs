namespace Smart.Data.SqlClient
{
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public sealed class SqlDialect : IDialect
    {
        public bool IsDuplicate(DbException ex)
        {
            return (ex as SqlException)?.Number == 2627;
        }

        public string LikeEscape(string value)
        {
            return Regex.Replace(value, @"[%_\[]", "[$0]");
        }
    }
}

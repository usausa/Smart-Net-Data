namespace Smart.Data.SqlClient
{
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    /// <summary>
    ///
    /// </summary>
    public class SqlDialect : IDialect
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public bool IsDuplicate(DbException ex)
        {
            return (ex as SqlException)?.Number == 2627;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string LikeEscape(string value)
        {
            return Regex.Replace(value, @"[%_\[]", "[$0]");
        }
    }
}

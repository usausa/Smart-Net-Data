namespace Smart.Data
{
    using System.Data.Common;

    public interface IDialect
    {
        bool IsDuplicate(DbException ex);

        string LikeEscape(string value);
    }
}

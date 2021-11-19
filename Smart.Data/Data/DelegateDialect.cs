namespace Smart.Data;

using System;
using System.Data.Common;

public sealed class DelegateDialect : IDialect
{
    private readonly Func<DbException, bool> isDuplicate;

    private readonly Func<string, string> likeEscape;

    public DelegateDialect(
        Func<DbException, bool> isDuplicate,
        Func<string, string> likeEscape)
    {
        this.isDuplicate = isDuplicate;
        this.likeEscape = likeEscape;
    }

    public bool IsDuplicate(DbException ex) => isDuplicate(ex);

    public string LikeEscape(string value) => likeEscape(value);
}

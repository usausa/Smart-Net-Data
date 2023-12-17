namespace Smart.Data;

using System;

#pragma warning disable CA1062
public static class DialectExtensions
{
    public static string? LikeStartWith(this IDialect dialect, string? value)
    {
        return String.IsNullOrEmpty(value) ? value : dialect.LikeEscape(value) + "%";
    }

    public static string? LikeEndWith(this IDialect dialect, string? value)
    {
        return String.IsNullOrEmpty(value) ? value : "%" + dialect.LikeEscape(value);
    }

    public static string? LikeContains(this IDialect dialect, string? value)
    {
        return String.IsNullOrEmpty(value) ? value : "%" + dialect.LikeEscape(value) + "%";
    }
}
#pragma warning restore CA1062

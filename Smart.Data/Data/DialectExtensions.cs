namespace Smart.Data
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
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
}

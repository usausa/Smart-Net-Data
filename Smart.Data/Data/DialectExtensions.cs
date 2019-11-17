namespace Smart.Data
{
    using System;

    public static class DialectExtensions
    {
        public static string LikeStartWith(this IDialect dialect, string value)
        {
            return String.IsNullOrEmpty(value) ? value : value + "%";
        }

        public static string LikeEndWith(this IDialect dialect, string value)
        {
            return String.IsNullOrEmpty(value) ? value : "%" + value;
        }

        public static string LikeContains(this IDialect dialect, string value)
        {
            return String.IsNullOrEmpty(value) ? value : "%" + value + "%";
        }
    }
}

namespace Smart.Data
{
    using System;
    using System.Data;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
    public static class DataRecordExtensions
    {
        public static bool? GetNullableBoolean(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (bool?)record.GetBoolean(i);
        }

        public static byte? GetNullableByte(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (byte?)record.GetByte(i);
        }

        public static char? GetNullableChar(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (char?)record.GetChar(i);
        }

        public static DateTime? GetNullableDateTime(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (DateTime?)record.GetDateTime(i);
        }

        public static decimal? GetNullableDecimal(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (decimal?)record.GetDecimal(i);
        }

        public static double? GetNullableDouble(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (double?)record.GetDouble(i);
        }

        public static float? GetNullableFloat(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (float?)record.GetFloat(i);
        }

        public static Guid? GetNullableGuid(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (Guid?)record.GetGuid(i);
        }

        public static short? GetNullableInt16(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (short?)record.GetInt32(i);
        }

        public static int? GetNullableInt32(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (int?)record.GetInt32(i);
        }

        public static long? GetNullableInt64(this IDataRecord record, int i)
        {
            return record.IsDBNull(i) ? null : (long?)record.GetInt64(i);
        }
    }
}

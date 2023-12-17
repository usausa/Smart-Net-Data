namespace Smart.Data;

using System;
using System.Data;

#pragma warning disable CA1062
public static class DataRecordExtensions
{
    public static bool? GetNullableBoolean(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetBoolean(i);
    }

    public static byte? GetNullableByte(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetByte(i);
    }

    public static char? GetNullableChar(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetChar(i);
    }

    public static DateTime? GetNullableDateTime(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetDateTime(i);
    }

    public static decimal? GetNullableDecimal(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetDecimal(i);
    }

    public static double? GetNullableDouble(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetDouble(i);
    }

    public static float? GetNullableFloat(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetFloat(i);
    }

    public static Guid? GetNullableGuid(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetGuid(i);
    }

    public static short? GetNullableInt16(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : (short?)record.GetInt32(i);
    }

    public static int? GetNullableInt32(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetInt32(i);
    }

    public static long? GetNullableInt64(this IDataRecord record, int i)
    {
        return record.IsDBNull(i) ? null : record.GetInt64(i);
    }
}
#pragma warning restore CA1062

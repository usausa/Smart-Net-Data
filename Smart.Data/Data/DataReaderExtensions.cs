namespace Smart.Data;

using System.Collections.Generic;
using System.Data;

#pragma warning disable CA1062
public static class DataReaderExtensions
{
    public static IEnumerable<IDataRecord> AsEnumerable(this IDataReader reader)
    {
        while (reader.Read())
        {
            yield return reader;
        }
    }
}
#pragma warning restore CA1062

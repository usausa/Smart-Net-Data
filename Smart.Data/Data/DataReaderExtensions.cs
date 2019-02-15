namespace Smart.Data
{
    using System.Collections.Generic;
    using System.Data;

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
}

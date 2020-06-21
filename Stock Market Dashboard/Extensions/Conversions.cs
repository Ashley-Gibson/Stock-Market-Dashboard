using System;

namespace Extensions
{
    public static class Conversions
    {
        public static long ConvertDateToUnix(DateTime date)
        {
            TimeSpan unixDate = date - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

            return (long)unixDate.TotalSeconds;
        }

        public static DateTime ConvertUnixToDate(long unixTimestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(unixTimestamp);
        }
    }
}

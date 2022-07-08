using System;
using System.Globalization;

namespace IS4
{
    public static class DateTimeUtils
    {
        public static DateTime GetEasterEuropeTime()
        {
            TimeZoneInfo easterEuropeTime =
                TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
            return
                TimeZoneInfo.ConvertTime(DateTime.Now, easterEuropeTime);
        }

        public static int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }

        public static string GetCurrentMonthName()
        {
            return
                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        }

        public static int GetCurrentMonthNumber()
        {
            return DateTime.Now.Month;
        }

        public static string GetCurrentMonthDate()
        {
            var result =
                GetEasterEuropeTime()
                    .ToString()
                    .Split(' ')[0]
                    .Split(new char[] { '-', '/' })[1];
            return result;
        }
    }
}

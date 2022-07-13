using System;
using System.Globalization;

namespace PasswordManagementApi.Infrastructure
{
    public static class DateTimeExtensions
    {
        private const string DateFormat = "yyyy-MM-dd'T'HH:mm:ss";
        
        public static DateTime ToDateTime(this string dateAsString)
        {
            return DateTime.ParseExact(dateAsString, DateFormat, CultureInfo.InvariantCulture);
        }

        public static string ToFormattedString(this DateTime date)
        {
            return date.ToString(DateFormat);
        }
    }
}

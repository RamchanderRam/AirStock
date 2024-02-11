using AirStock.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirStock.Common.Models
{
    public class DateRoutine
    {
        public static DateTimeOffset GetCurrentDateTime()
        {
            // DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            return DateTimeOffset.Now;
        }
        public static DateTime GetCurrentDate()
        {
            return DateTime.UtcNow;
        }
    }
    public static class DateFunction
    {
        public static DateTime? GetDate(this DateTime? value)
        {
            if (value == null || value.Value == DateTime.MinValue)
            {
                return null;
            }
            return value.Value;
        }
        public static DateTime? GetDate(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return Convert.ToDateTime(value);
        }
        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
        public static DateTime ConvertToUTC(this DateTime theDate, string sourceTimeZone)
        {
            try
            {
                sourceTimeZone = TimeZoneAbbrModel.GetDisplayName(sourceTimeZone);
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZone);
                DateTime sourceUTCTime = TimeZoneInfo.ConvertTimeToUtc(theDate, timeZoneInfo);
                return sourceUTCTime;
            }
            catch
            {
                return theDate;
            }
        }
        public static DateTime ConvertFromUTC(this DateTime theDate, string destinationTimeZone)
        {
            try
            {
                destinationTimeZone = TimeZoneAbbrModel.GetDisplayName(destinationTimeZone);
                TimeZoneInfo destTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZone);
                DateTime destinationUTCTime = TimeZoneInfo.ConvertTimeFromUtc(theDate, destTimeZoneInfo);
                return destinationUTCTime;
            }
            catch
            {
                return theDate;
            }
        }
    }
}

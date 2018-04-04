using System;

namespace Hubris
{
    /// <summary>
    /// A collection of time related helpers.
    /// </summary>
	public struct Khronos
    {
        /// <summary>
        /// DateTime for Thursday, 1 January 1970.
        /// </summary>
        private static DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Return the current time in UnixTime.
        /// </summary>
        public static int UnixTimeNow()
        {
            return (int) DateTime.UtcNow.Subtract(unixStart).TotalSeconds;
        }

        /// <summary>
        /// Converts UnixTime to DateTime.
        /// </summary>
        public static DateTime UnixTimeToDateTime(int moment)
        {
            return unixStart.AddSeconds(moment).ToLocalTime();
        }

        /// <summary>
        /// Converts DateTime to UnixTime.
        /// </summary>
        public static int DateTimeToUnixTime(DateTime moment)
        {
            return (int) moment.Subtract(unixStart).TotalSeconds;
        }

        /// <summary>
        /// Gets the full compact string date.
        /// </summary>
        private static string GetDate(DateTime moment, bool getYearMonthDay, bool getHourMinuteSecond)
		{
			string returnDate = "";
            returnDate += getYearMonthDay ? moment.Year + "_" + SetDatePadLeft(moment.Month) + "_" + SetDatePadLeft(moment.Day) : "";
            returnDate += (getYearMonthDay && getHourMinuteSecond) ? "-" : "";
            returnDate += getHourMinuteSecond ? SetDatePadLeft(moment.Hour) + "_" + SetDatePadLeft(moment.Minute) + "_" + SetDatePadLeft(moment.Second) : "";

			return returnDate;
		}

        /// <summary>
        /// Pads zero to left.
        /// </summary>
        private static string SetDatePadLeft(int dateTime)
        {
            return dateTime.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Gets the full compact string date.
        /// </summary>
        public static string GetDateCompact(DateTime moment)
		{
			return GetDate(moment, true, true);
		}

		/// <summary>
		/// Gets the compact string of Year, Month and Day.
		/// </summary>
		public static string GetYMDCompact(DateTime moment)
		{
			return GetDate(moment, true, false);
		}

		/// <summary>
		/// Gets the compact string of Hour, Minute and Second.
		/// </summary>
		public static string GetHMSCompact(DateTime moment)
		{
			return GetDate(moment, false, true);
		}
	}
}
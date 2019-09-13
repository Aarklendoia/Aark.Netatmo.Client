using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Helpers
{
    /// <summary>
    /// Tools to convert Netatmo datetime format to local datetime.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert a <paramref name="value"/> to a localised <see cref="DateTime"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="DateTime"/> corresponding to the input <paramref name="value"/></returns>
        public static DateTime ToLocalDateTime(this long value)
        {
            return DateTimeOffset.FromUnixTimeSeconds(value).DateTime.ToLocalTime();
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a localised <see cref="DateTime"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="DateTime"/> corresponding to the input <paramref name="value"/></returns>
        public static DateTime ToLocalDateTime(this long? value)
        {
            if (value != null)
                return DateTimeOffset.FromUnixTimeSeconds(value.Value).DateTime.ToLocalTime();
            else
                return DateTime.MinValue;
        }

        /// <summary>
        /// Convert a localised <see cref="DateTime"/> to a Netatmo server <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <paramref name="value"/> corresponding to the input <see cref="DateTime"/></returns>
        public static long FromLocalDateTime(this DateTime value)
        {
            DateTime baseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(value - baseTime).TotalSeconds;
        }

        /// <summary>
        /// Convert a localised <see cref="DateTime"/> to a Netatmo server <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <paramref name="value"/> corresponding to the input <see cref="DateTime"/></returns>
        public static long FromLocalDateTime(this DateTime? value)
        {
            DateTime baseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return value == null ? 0 : (long)((DateTime)value - baseTime).TotalSeconds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Helpers.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
                return dateTime;

            // Do not modify "guard" values.
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MinValue)
                return dateTime; 

            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
    }
}

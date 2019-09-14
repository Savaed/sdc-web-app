using System;
using System.ComponentModel.DataAnnotations;

namespace SDCWebApp.Models
{
    public class OpeningHours : BasicEntity
    {
        [DataType("time(0)")]
        public TimeSpan OpeningHour { get; set; }
        [DataType("time(0)")]
        public TimeSpan ClosingHour { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public virtual GeneralSightseeingInfo Info { get; set; }
    }
}

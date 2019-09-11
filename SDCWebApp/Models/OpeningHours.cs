using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

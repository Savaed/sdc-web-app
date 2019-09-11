using System;

namespace SDCWebApp.Models.Dtos
{
    public class OpeningHoursDto
    {
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
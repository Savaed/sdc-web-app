using System;

namespace SDCWebApp.Models.Dtos
{
    public class ActivityLogDto
    {
        public string Id { get; set; }
        public DateTime Date { get; private set; } = DateTime.Now;
        public string User { get; set; }
        public ActivityLog.ActivityType Type { get; set; }
        public string Description { get; set; }
    }
}

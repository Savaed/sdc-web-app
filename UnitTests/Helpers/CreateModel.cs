using SDCWebApp.Models;
using System;

namespace UnitTests.Helpers
{
    public static class CreateModel
    {
        public static VisitInfo CreateInfo(string id = "1", string description = "test", int maxChildAge = 5, int maxAllowedGroupSize = 30,
            int maxTicketOrderInterval = 4, int sightseeingDuration = 2)
        {
            return new VisitInfo
            {
                Id = id,
                Description = description,
                MaxAllowedGroupSize = maxAllowedGroupSize,
                MaxChildAge = maxChildAge,
                MaxTicketOrderInterval = maxTicketOrderInterval,
                SightseeingDuration = sightseeingDuration,
                OpeningHours = CreateOpenigHoursInWeek()
            };
        }


        #region Privates

        private static OpeningHours[] CreateOpenigHoursInWeek()
        {
            return new OpeningHours[]
            {
                new OpeningHours{ Id = "1", ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Monday },
                new OpeningHours{ Id = "2", ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Tuesday },
                new OpeningHours{ Id = "3", ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Wednesday },
                new OpeningHours{ Id = "4", ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Thursday },
                new OpeningHours{ Id = "5", ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Friday },
                new OpeningHours{ Id = "6", ClosingHour = new TimeSpan(16,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Saturday },
                new OpeningHours{ Id = "7", ClosingHour = new TimeSpan(16,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Sunday }
            };
        }

        #endregion

    }
}

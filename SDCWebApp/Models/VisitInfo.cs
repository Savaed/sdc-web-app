using System;
using System.Collections.Generic;
using System.Linq;

namespace SDCWebApp.Models
{
    public class VisitInfo : BasicEntity, ICloneable, IEquatable<VisitInfo>
    {
        public string Description { get; set; }
        public int MaxChildAge { get; set; }
        public int MaxAllowedGroupSize { get; set; }
        public float SightseeingDuration { get; set; } // In hours
        public int MaxTicketOrderInterval { get; set; } // In weeks.

        // List that contains DaysOfWeek and OpeningHour, ClosingHour eg.
        // Sunday = [ Sunday, 10:00, 18:00 ]
        // Friday = [ Friday, 8:00, 16:00 ]
        public virtual ICollection<OpeningHours> OpeningHours { get; set; }


        /// <summary>
        /// Returns company closing <see cref="DateTime"/> based on <paramref name="dateTime"/> date and stored sightseeing info.
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> for which closing hour will be calculated.</param>
        public DateTime GetClosingDateTime(DateTime dateTime)
        {
            var closingHour = OpeningHours.ToList().Single(x => x.DayOfWeek == dateTime.DayOfWeek).ClosingHour;
            return dateTime.Date.AddTicks(closingHour.Ticks);
        }

        /// <summary>
        /// Returns company opening <see cref="DateTime"/> based on <paramref name="dateTime"/> date and stored sightseeing info.
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> for which opening hour will be calculated.</param>
        public DateTime GetOpeningDateTime(DateTime dateTime)
        {
            var openingHour = OpeningHours.ToList().Single(x => x.DayOfWeek == dateTime.DayOfWeek).OpeningHour;
            return dateTime.Date.AddTicks(openingHour.Ticks);
        }


        #region Helper methods

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(VisitInfo other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Description == other.Description
                && MaxAllowedGroupSize == other.MaxAllowedGroupSize
                && MaxChildAge == other.MaxChildAge
                && MaxTicketOrderInterval == other.MaxTicketOrderInterval
                && SightseeingDuration == other.SightseeingDuration;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as VisitInfo);
        }

        public override int GetHashCode()
        {
            if (Description is null)
            {
                return base.GetHashCode();
            }

            return (Description.GetHashCode() + MaxAllowedGroupSize + MaxChildAge + MaxTicketOrderInterval + (int)SightseeingDuration) * 0x00010000;
        }

        #endregion

    }
}

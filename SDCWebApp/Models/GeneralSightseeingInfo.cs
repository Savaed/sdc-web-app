using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class GeneralSightseeingInfo : BasicEntity, ICloneable, IEquatable<GeneralSightseeingInfo>
    {
        public string Description { get; set; }
        public int MaxChildAge { get; set; }
        public int MaxAllowedGroupSize { get; set; }
        [DataType("time(0)")]
        public TimeSpan OpeningHour { get; set; }
        [DataType("time(0)")]
        public TimeSpan ClosingHour { get; set; }
        public int MaxTicketOrderInterval { get; set; } // In weeks.


        [NotMapped]
        public DateTime OpeningDateTime { get => new DateTime(OpeningHour.Ticks); }

        [NotMapped]
        public DateTime ClosingDateTime { get => new DateTime(ClosingHour.Ticks); }


        #region Helper methods

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(GeneralSightseeingInfo other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
                return true;

            return Description == other.Description
                && ClosingHour == other.ClosingHour
                && OpeningHour == other.OpeningHour
                && MaxAllowedGroupSize == other.MaxAllowedGroupSize
                && MaxChildAge == other.MaxChildAge;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GeneralSightseeingInfo);
        }

        public override int GetHashCode()
        {
            if (Description is null)
                return base.GetHashCode();

            return (Description.GetHashCode() + ClosingHour.GetHashCode() + OpeningHour.GetHashCode() + MaxAllowedGroupSize + MaxChildAge) * 0x00010000;
        }

        #endregion
    }
}

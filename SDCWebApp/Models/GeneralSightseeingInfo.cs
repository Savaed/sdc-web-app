using System;

namespace SDCWebApp.Models
{
    public class GeneralSightseeingInfo : BasicEntity, ICloneable, IEquatable<GeneralSightseeingInfo>
    {
        public string Description { get; set; }
        // 0-18
        public int MaxChildAge { get; set; }

        // 0-iles tam
        public int MaxAllowedGroupSize { get; set; }

        // pelna lub polowa
        public float OpeningHour { get; set; }
        // jw
        public float ClosingHour { get; set; }

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

            return (Description.GetHashCode() + (int)ClosingHour + (int)OpeningHour + MaxAllowedGroupSize + MaxChildAge) * 0x00010000;
        }
    }
}

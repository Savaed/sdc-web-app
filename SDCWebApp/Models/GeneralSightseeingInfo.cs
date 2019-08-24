using System;

namespace SDCWebApp.Models
{
    public class GeneralSightseeingInfo : BasicEntity, ICloneable
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
    }
}

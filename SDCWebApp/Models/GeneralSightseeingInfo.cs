using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models
{
    public class GeneralSightseeingInfo : BasicEntity
    {
        // 0-18
        public int MaxChildAge { get; set; }

        // 0-iles tam
        public int MaxGroupSize { get; set; }

        // pelna lub polowa
        public float OpeningHour { get; set; }
        // jw
        public float ClosingHour { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDCWebApp.Models
{
    public class SightseeingTariff : BasicEntity, ICloneable
    {
        public string Name { get; set; }

        public virtual ICollection<TicketTariff> TicketTariffs { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

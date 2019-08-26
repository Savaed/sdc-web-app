using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SDCWebApp.Models
{
    public class SightseeingTariff : BasicEntity, ICloneable
    {
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<TicketTariff> TicketTariffs { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

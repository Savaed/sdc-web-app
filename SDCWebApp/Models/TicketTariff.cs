using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SDCWebApp.Models
{
    public class TicketTariff : BasicEntity, ICloneable
    {
        public string Description { get; set; }
        public bool IsPerHour { get; set; } = false;
        public bool IsPerPerson { get; set; } = true;
        // Ticket price without any discount
        public float DefaultPrice { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual SightseeingTariff SightseeingTariff { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
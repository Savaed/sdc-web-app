using System.Collections.Generic;

namespace SDCWebApp.Models
{
    public class TicketTariff : BasicEntity
    {
        public string Description { get; set; }
        public bool IsPerHour { get; set; } = false;
        public bool IsPerPerson { get; set; } = true;

        // Ticket price without any discount
        public float DefaultPrice { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual SightseeingTariff SightseeingTariff { get; set; }
    }
}
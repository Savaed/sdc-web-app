using System;
using System.Collections.Generic;

namespace SDCWebApp.Models
{
    public class TicketTariff : BasicEntity, ICloneable, IEquatable<TicketTariff>
    {
        public string Description { get; set; }
        public bool IsPerHour { get; set; } = false;
        public bool IsPerPerson { get; set; } = true;
        // Ticket price without any discount
        public float DefaultPrice { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual VisitTariff VisitTariff { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(TicketTariff other)
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
                && IsPerHour == other.IsPerHour
                && IsPerPerson == other.IsPerPerson
                && DefaultPrice == other.DefaultPrice;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TicketTariff);
        }

        public override int GetHashCode()
        {
            if (Description is null)
            {
                return base.GetHashCode();
            }

            return (Description.GetHashCode() + IsPerHour.GetHashCode() + IsPerPerson.GetHashCode() + DefaultPrice.GetHashCode()) * 0x00000001;
        }
    }
}
using System;
using System.Collections.Generic;

namespace SDCWebApp.Models
{
    public class SightseeingTariff : BasicEntity, ICloneable, IEquatable<SightseeingTariff>
    {
        public string Name { get; set; }
        public virtual ICollection<TicketTariff> TicketTariffs { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SightseeingTariff);
        }

        public bool Equals(SightseeingTariff tariff)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(tariff, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (ReferenceEquals(this, tariff))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (GetType() != tariff.GetType())
            {
                return false;
            }

            return Name == tariff.Name;
        }

        public override int GetHashCode()
        {
            if (Name is null)
            {
                return base.GetHashCode() * 0x00011000;
            }

            return Name.GetHashCode() * 0x00011000;
        }
    }
}

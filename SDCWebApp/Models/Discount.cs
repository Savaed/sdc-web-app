using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDCWebApp.Models
{
    public class Discount : BasicEntity, ICloneable
    {
        public enum DiscountType
        {
            ForChild,
            ForPensioner,
            ForStudent,
            ForDisabled,
            ForGroup,
            ForFamily
        }

        public DiscountType Type { get; set; }
        public string Description { get; set; }
        public int DiscountValueInPercentage { get; set; }
        public int? GroupSizeForDiscount { get; set; } = null;

        public virtual ICollection<Ticket> Tickets { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            string[] uniqueProperties = new string[] { "Description" };
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                if (uniqueProperties.Any(x => x.Equals(property.Name)))
                {
                    return property.GetValue(this) == property.GetValue(obj);
                }
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }
    }   
}

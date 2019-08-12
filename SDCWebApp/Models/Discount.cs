using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}

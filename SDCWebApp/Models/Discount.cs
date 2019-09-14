using System;
using System.Collections.Generic;

namespace SDCWebApp.Models
{
    public class Discount : BasicEntity, ICloneable, IEquatable<Discount>
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

        public DiscountType Type { get; set; } = DiscountType.ForGroup;
        public string Description { get; set; }
        public int DiscountValueInPercentage { get; set; }
        public int? GroupSizeForDiscount { get; set; } = null;

        public virtual ICollection<Ticket> Tickets { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Discount discount)
        {
            if (discount is null || GetType() != discount.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, discount))
            {
                return true;
            }

            return Type == discount.Type && Description == discount.Description && DiscountValueInPercentage == discount.DiscountValueInPercentage &&
                   (GroupSizeForDiscount is null ? false : GroupSizeForDiscount == discount.GroupSizeForDiscount);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Discount);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            if (Description is null | GroupSizeForDiscount is null)
            {
                return base.GetHashCode();
            }

            return Type.GetHashCode() + Description.GetHashCode() + DiscountValueInPercentage.GetHashCode() + GroupSizeForDiscount.Value.GetHashCode();
        }
    }
}

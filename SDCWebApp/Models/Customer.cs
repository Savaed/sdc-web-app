using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class Customer : BasicEntity, ICloneable, IEquatable<Customer>
    {
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; } = DateTime.MinValue;
        public bool IsChild { get; set; }
        public bool IsDisabled { get; set; }
        public bool HasFamilyCard { get; set; }
        public string EmailAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Ticket> Tickets { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Customer other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EmailAddress == other.EmailAddress;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Customer);
        }

        public override int GetHashCode()
        {
            if (EmailAddress is null)
            {
                return base.GetHashCode();
            }

            return EmailAddress.GetHashCode() * 0x10000000;
        }
    }
}

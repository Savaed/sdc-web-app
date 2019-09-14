using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class SightseeingGroup : BasicEntity, ICloneable, IEquatable<SightseeingGroup>
    {
        // Time must be full hour
        [Required, Column(TypeName = "datetime2(0)")]
        public DateTime SightseeingDate { get; set; }
        // < max allowed group size
        public int MaxGroupSize { get; set; } = 30;
        public int CurrentGroupSize
        {
            get => Tickets is null ? 0 : Tickets.Count;
            private set { }
        }
        public bool IsAvailablePlace
        {
            get => CurrentGroupSize < MaxGroupSize;
            private set { }
        }
        public virtual ICollection<Ticket> Tickets { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(SightseeingGroup other)
        {
            if (other is null || GetType() != other.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return SightseeingDate == other.SightseeingDate;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SightseeingGroup);
        }

        public override int GetHashCode()
        {
            return SightseeingDate.GetHashCode() * 0x00010000;
        }
    }
}

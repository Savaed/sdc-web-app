using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class SightseeingGroup : BasicEntity, ICloneable, IEquatable<SightseeingGroup>
    {
        private bool _isAvailablePlace = true;
        private int _currentGroupSize = 0;


        // Time must be full hour
        [Required, Column(TypeName = "datetime2(0)")]
        public DateTime SightseeingDate { get; set; }
        // < max allowed group size
        public int MaxGroupSize { get; set; } = 30;
        public int CurrentGroupSize
        {
            get => Tickets is null ? _currentGroupSize : Tickets.Count;
            private set => _currentGroupSize = value;
        }
        public bool IsAvailablePlace
        {
            get => CurrentGroupSize < MaxGroupSize;
            private set => _isAvailablePlace = value;
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
                return true;

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

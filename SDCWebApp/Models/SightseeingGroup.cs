using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class SightseeingGroup : BasicEntity, ICloneable
    {
        private bool _isAvailablePlace;
        private int _currentGroupSize;


        // Time must be full hour
        [Required, Column(TypeName = "datetime2(0)")]
        public DateTime SightseeingDate { get; set; }
        // < max allowed group size
        public int MaxGroupSize { get; set; } = 30;
        public int CurrentGroupSize => _currentGroupSize = Tickets != null ? Tickets.Count : _currentGroupSize;
        public bool IsAvailablePlace => _isAvailablePlace = CurrentGroupSize < MaxGroupSize;
        public virtual ICollection<Ticket> Tickets { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class Customer : BasicEntity, ICloneable
    {
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; } = DateTime.MinValue;
        public bool IsChild { get; set; }
        public bool IsDisabled { get; set; }
        public bool HasFamilyCard { get; set; }
        public string EmailAddres { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class Customer : BasicEntity
    {
        private bool _isChild = false;


        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; } = DateTime.MinValue;

        //public bool IsChild
        //{
        //    get => _isChild = (DateOfBirth != DateTime.MinValue && ChildDescription != null) ? DateOfBirth.AddYears(ChildDescription.MaxChildAge) > DateTime.Now.ToLocalTime() : false;
        //    private set => _isChild = value;
        //}

        public bool IsDisabled { get; set; }

        public bool HasFamilyCard { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(30)]
        public string EmailAddres { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

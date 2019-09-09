using System;

namespace SDCWebApp.Models.Dtos
{
    public class CustomerDto : DtoBase
    {       
        public DateTime? DateOfBirth { get; set; }
        public bool IsChild { get; set; }
        public bool IsDisabled { get; set; }
        public bool HasFamilyCard { get; set; }
        public string EmailAddress { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class DiscountDto : DtoBase
    {      
        public Discount.DiscountType Type { get; set; }
        public string Description { get; set; }
        public int DiscountValueInPercentage { get; set; }
        public int? GroupSizeForDiscount { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}

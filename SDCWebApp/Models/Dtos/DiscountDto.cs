using Newtonsoft.Json;
using System.Collections.Generic;

namespace SDCWebApp.Models.Dtos
{
    public class DiscountDto : DtoBase
    {
        public Discount.DiscountType Type { get; set; }
        public string Description { get; set; }
        public int DiscountValueInPercentage { get; set; }
        public int? GroupSizeForDiscount { get; set; }
    }
}

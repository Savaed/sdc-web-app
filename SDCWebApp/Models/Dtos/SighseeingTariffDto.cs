using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class SightseeingTariffDto : DtoBase
    {
        public string Name { get; set; }
        public ICollection<TicketTariffDto> TicketTariffs { get; set; }
    }
}

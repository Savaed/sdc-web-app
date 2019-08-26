using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class SighseeingTariffDto : DtoBase
    {
        public string Name { get; set; }
        public IEnumerable<TicketTariffDto> TicketTariffs { get; set; }
    }
}

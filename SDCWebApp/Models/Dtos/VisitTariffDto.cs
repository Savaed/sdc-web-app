using System.Collections.Generic;

namespace SDCWebApp.Models.Dtos
{
    public class VisitTariffDto : DtoBase
    {
        public string Name { get; set; }
        public ICollection<TicketTariffDto> TicketTariffs { get; set; }
    }
}

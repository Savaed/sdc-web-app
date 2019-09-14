using System.Collections.Generic;

namespace SDCWebApp.Models.Dtos
{
    public class VisitInfoDto : DtoBase
    {
        public string Description { get; set; }
        public int MaxChildAge { get; set; }
        public int MaxAllowedGroupSize { get; set; }
        public float SightseeingDuration { get; set; }
        public int MaxTicketOrderInterval { get; set; }
        public IEnumerable<OpeningHoursDto> OpeningHours { get; set; }
    }
}

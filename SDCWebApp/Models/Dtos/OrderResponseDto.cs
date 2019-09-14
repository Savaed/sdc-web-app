using System.Collections.Generic;

namespace SDCWebApp.Models.Dtos
{
    public class OrderResponseDto
    {
        public string Id { get; set; }
        public CustomerDto Customer { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}

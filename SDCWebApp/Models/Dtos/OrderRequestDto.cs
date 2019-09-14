using System.Collections.Generic;

namespace SDCWebApp.Models.Dtos
{
    public class OrderRequestDto
    {
        public string Id { get; set; }
        public CustomerDto Customer { get; set; }
        public IEnumerable<ShallowTicket> Tickets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }
        public CustomerDto Customer { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}

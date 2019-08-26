using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class TicketTariffDto : DtoBase
    {       
        public string Description { get; set; }
        public bool IsPerHour { get; set; }
        public bool IsPerPerson { get; set; }
        public float DefaultPrice { get; set; }
    }
}

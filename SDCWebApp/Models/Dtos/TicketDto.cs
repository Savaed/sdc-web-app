using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SDCWebApp.Models.Dtos
{
    public class TicketDto : DtoBase
    {
        public string TicketUniqueId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidFor { get; set; }
        public float Price { get; set; }

        [JsonProperty("_links")]
        public IEnumerable<ApiLink> Links { get; set; }
    }
}

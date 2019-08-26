using Newtonsoft.Json;
using System;

namespace SDCWebApp.Models.Dtos
{
    public class DtoBase
    {
        [JsonProperty(Order = 1)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
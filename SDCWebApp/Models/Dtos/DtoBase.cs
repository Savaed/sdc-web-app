using Newtonsoft.Json;
using System;

namespace SDCWebApp.Models.Dtos
{
    public class DtoBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
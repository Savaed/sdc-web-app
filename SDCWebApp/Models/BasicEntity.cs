using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SDCWebApp.Models
{
    public abstract class BasicEntity : IEntity
    {
        [Required]
        [JsonProperty(Order = 1)]
        public string Id { get; set; }

        [Timestamp]
        public byte[] ConcurrencyToken { get; set; }

        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}

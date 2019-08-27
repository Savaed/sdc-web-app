using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SDCWebApp.Models
{
    public abstract class BasicEntity : IEntity
    {
        [Required]
        public string Id { get; set; }

        [Timestamp]
        public byte[] ConcurrencyToken { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}

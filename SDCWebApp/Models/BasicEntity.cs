using System;
using System.ComponentModel.DataAnnotations;

namespace SDCWebApp.Models
{
    public abstract class BasicEntity : IEntity
    {
        [Required]
        public string Id { get; set; }

        [Timestamp]
        [Required]
        public byte[] ConcurrencyToken { get; set; }

        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [Required]
        public DateTime? UpdatedAt { get; set; } = null;
    }
}

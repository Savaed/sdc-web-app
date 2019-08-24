using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class Ticket : BasicEntity, ICloneable
    {
        private float _price = 0.0f;


        public string TicketUniqueId { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        [Column(TypeName = "date")]
        public DateTime ValidFor { get; set; }

        // Ticket price calculated basic on default price in TicketTariff and Discount.
        public float Price
        {
            get
            {
                if (Tariff != null && Discount != null)
                    _price = Tariff.DefaultPrice * (1.0f - (Discount.DiscountValueInPercentage / 100.0f));
                else if (Tariff != null)
                    _price = Tariff.DefaultPrice;
                return _price;
            }
            private set => _price = value;
        }

        public virtual TicketTariff Tariff { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual SightseeingGroup Group { get; set; }
        public virtual Customer Customer { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

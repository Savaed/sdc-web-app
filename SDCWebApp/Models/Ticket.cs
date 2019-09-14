using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDCWebApp.Models
{
    public class Ticket : BasicEntity, ICloneable
    {
        public string TicketUniqueId { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime2(0)")]
        public DateTime ValidFor
        {
            get
            {
                return Group is null ? DateTime.MinValue : Group.SightseeingDate;
            }

            private set { }
        }

        public string OrderStamp { get; set; }

        // Ticket price calculated based on default price in TicketTariff and Discount value in percentage.
        public float Price
        {
            get
            {
                if (Tariff != null && Discount != null)
                {
                    return Tariff.DefaultPrice * (1.0f - (Discount.DiscountValueInPercentage / 100.0f));
                }
                else if (Tariff != null)
                {
                    return Tariff.DefaultPrice;
                }

                return 0.0f;
            }
            private set { }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual TicketTariff Tariff { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Discount Discount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual SightseeingGroup Group { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Customer Customer { get; set; }


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

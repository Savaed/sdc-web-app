using System;

namespace SDCWebApp.Models.Dtos
{
    public class ShallowTicket
    {
        public DateTime SightseeingDate { get; set; }
        public string TicketTariffId { get; set; }
        public string DiscountId { get; set; }
    }
}
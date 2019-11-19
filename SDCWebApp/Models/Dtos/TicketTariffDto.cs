namespace SDCWebApp.Models.Dtos
{
    public class TicketTariffDto : DtoBase
    {
        public string VisitTariffId { get; set; }
        public string Description { get; set; }
        public bool IsPerHour { get; set; }
        public bool IsPerPerson { get; set; }
        public float DefaultPrice { get; set; }
    }
}

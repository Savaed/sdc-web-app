namespace SDCWebApp.Models
{
    public class GeneralSightseeingInfo : BasicEntity
    {
        public string Description { get; set; }
        // 0-18
        public int MaxChildAge { get; set; }

        // 0-iles tam
        public int MaxAllowedGroupSize { get; set; }

        // pelna lub polowa
        public float OpeningHour { get; set; }
        // jw
        public float ClosingHour { get; set; }
    }
}

namespace SDCWebApp.Models.Dtos
{
    public class ApiLink
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }


        public ApiLink(string resourceName, string urlToResource, string method)
        {
            Rel = resourceName;
            Href = urlToResource;
            Method = method;
        }
    }
}

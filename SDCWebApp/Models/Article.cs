using System;

namespace SDCWebApp.Models
{
    public class Article : BasicEntity, ICloneable
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; } = $"SDC { DateTime.Now.Year.ToString() }";

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

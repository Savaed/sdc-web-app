using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos
{
    public class ArticleDto : DtoBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
    }
}

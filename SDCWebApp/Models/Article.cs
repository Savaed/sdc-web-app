using System;

namespace SDCWebApp.Models
{
    public class Article : BasicEntity, ICloneable, IEquatable<Article>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; } = $"SDC { DateTime.Now.Year.ToString() }";

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Article);
        }

        public bool Equals(Article article)
        {
            // If parameter is null, return false.
            if (article is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (ReferenceEquals(this, article))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (GetType() != article.GetType())
            {
                return false;
            }

            return Title == article.Title && Text == article.Text && Author == article.Author;
        }

        public override int GetHashCode()
        {
            if (Author is null || Text is null || Title is null)
                return base.GetHashCode();
            return Title.GetHashCode() * 0x00011000 + Text.GetHashCode() * 0x10000000 + Author.GetHashCode() * 0x00000001;
        }
    }
}

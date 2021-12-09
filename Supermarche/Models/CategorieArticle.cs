using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Supermarche.Models
{
    public partial class CategorieArticle
    {
        public CategorieArticle()
        {
            Article = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Label { get; set; }

        public virtual ICollection<Article> Article { get; set; }
    }
}

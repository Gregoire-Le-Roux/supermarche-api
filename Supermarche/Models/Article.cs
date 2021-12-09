using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Supermarche.Models
{
    public partial class Article
    {
        public Article()
        {
            Panier = new HashSet<Panier>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public int? Quantite { get; set; }
        public decimal? Prix { get; set; }
        public int? IdCategorie { get; set; }

        public virtual CategorieArticle IdCategorieNavigation { get; set; }
        public virtual ICollection<Panier> Panier { get; set; }
    }
}

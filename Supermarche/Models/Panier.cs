using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Supermarche.Models
{
    public partial class Panier
    {
        public int IdPanier { get; set; }
        public int IdClient { get; set; }
        public int IdArticle { get; set; }
        public int Quantite { get; set; }
        public DateTime? DateAchat { get; set; }

        public virtual Article IdArticleNavigation { get; set; }
        public virtual Client IdClientNavigation { get; set; }
    }
}

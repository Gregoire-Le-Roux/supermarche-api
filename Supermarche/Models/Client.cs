using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Supermarche.Models
{
    public partial class Client
    {
        public Client()
        {
            Panier = new HashSet<Panier>();
        }

        public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public decimal? Argent { get; set; }

        public virtual ICollection<Panier> Panier { get; set; }
    }
}

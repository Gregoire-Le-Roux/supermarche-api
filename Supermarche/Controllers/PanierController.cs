using Microsoft.AspNetCore.Mvc;
using Supermarche.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarche.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanierController : ControllerBase
    {
        private SupermarcheContext context;
        public PanierController(SupermarcheContext _context )
        {
            context = _context;
        }

        [HttpGet("allPanier")]
        public string allPanier()
        {
            var panier =
                (from p in context.Panier 
                select new { p.IdPanier, p.IdClient, p.IdClientNavigation.Nom, p.IdClientNavigation.Prenom, p.IdArticleNavigation.IdCategorieNavigation.Label, p.IdArticle ,Prix = p.IdArticleNavigation.Prix, NomArticle = p.IdArticleNavigation.Nom, PrixTotal = p.IdArticleNavigation.Prix * p.Quantite ,p.Quantite, p.DateAchat});
            return JsonConvert.SerializeObject(panier);
        }

        [HttpGet("byPanier/{idPanier}")]
        public string ByPanier(string idPanier = "1")
        {
            int idPan = int.Parse(idPanier);
            var allPanier =
                (from p in context.Panier
                 where p.IdPanier == idPan
                 select new { p.IdPanier, p.IdClientNavigation.Nom, p.IdClientNavigation.Prenom, p.IdArticleNavigation.IdCategorieNavigation.Label, p.Quantite, p.DateAchat });
            return JsonConvert.SerializeObject(allPanier);
        }

        [HttpGet("byClientPanier/{idClient}")]
        public string ByClientPanier(string idClient = "1")
        {
            int idCli = int.Parse(idClient);
            var allPanier =
                (from p in context.Panier
                 where p.IdClient== idCli
                 select new { p.IdPanier, p.IdClientNavigation.Nom, p.IdClientNavigation.Prenom, p.IdArticleNavigation.IdCategorieNavigation.Label, p.Quantite, p.DateAchat });
            return JsonConvert.SerializeObject(allPanier);
        }

        [HttpPost("addPanier")]
        public void AddPanier(Panier panier)
        {
            context.Panier.Add(panier);
            context.SaveChanges();
        }

        [HttpPost("modifArticlePanier")]
        public void ModifArticlePanier(Panier panier)
        {
            int idPan = panier.IdPanier;
            int idArt = panier.IdArticle;
            var modifArtPan =
                (from p in context.Panier
                 where p.IdPanier == idPan && p.IdArticle == idArt
                 select new { p.IdPanier, p.IdClient, p.IdArticle, p.IdClientNavigation.Nom, p.IdClientNavigation.Prenom, p.IdArticleNavigation.IdCategorieNavigation.Label, p.Quantite, p.DateAchat}).ToList();
            if (modifArtPan != null)
            {
                int newQuantite = modifArtPan[0].Quantite - panier.Quantite;
                Panier updPanier = new Panier() { IdPanier = modifArtPan[0].IdPanier, IdClient = modifArtPan[0].IdClient, IdArticle = modifArtPan[0].IdArticle, Quantite = newQuantite, DateAchat = modifArtPan[0].DateAchat };
                context.Panier.Update(updPanier);
                context.SaveChanges();
            }            
        }

        [HttpPost("achatPanier")]
        public void AchatPanier([FromBody] Panier panier)
        {
            int idPan = panier.IdPanier;
            var allPanier =
                (from p in context.Panier
                 where p.IdPanier == idPan
                 select new { p.IdPanier, p.IdClient, p.IdArticle, p.IdClientNavigation.Nom, p.IdClientNavigation.Prenom, p.IdArticleNavigation.IdCategorieNavigation.Label, p.Quantite, p.DateAchat, QuantiteArticle = p.IdArticleNavigation.Quantite });
            var idCli = panier.IdClient;
            var clientArgent =
                (from c in context.Client
                 where c.Id == idCli
                 select c).ToList();
            foreach (var pan in allPanier)
            {
                var article =
                    (from a in context.Article
                     where a.Id == pan.IdArticle
                     select new { a.Id, a.Nom, a.Prix, a.Quantite }).ToList();

                Panier updPanier = new Panier() { IdPanier = pan.IdPanier, IdClient = pan.IdClient, IdArticle = pan.IdArticle, Quantite = pan.Quantite, DateAchat = DateTime.Now };
                context.Panier.Update(updPanier);
                int newQuantiteArticle = (int)pan.QuantiteArticle - pan.Quantite;
                Article updArticle = new Article() { Id = pan.IdArticle, Nom = article[0].Nom, Prix = article[0].Prix, Quantite = newQuantiteArticle };
                context.Article.Update(updArticle);
                decimal argentClient = (decimal)clientArgent[0].Argent - ((decimal)article[0].Prix * (decimal)pan.Quantite);
                Client updClient = new Client() { Id = idCli, Nom = clientArgent[0].Nom, Prenom = clientArgent[0].Prenom, Argent = argentClient };
                context.Client.Update(updClient);
            }
            context.SaveChanges();
        }

        [HttpDelete("deletePanier/{idPanier}")]
        public void DeletePanier(string idPanier)
        {
            int idPan = int.Parse(idPanier);
            var remove =
                from p in context.Panier
                 where p.IdPanier == idPan
                 select p;
            foreach (var rem in remove)
            {
                context.Panier.Remove(rem);
            }
            context.SaveChanges();
        }

        [HttpDelete("deleteArticlePanier/{idPanier}/{idArticle}")]
        public void DeleteArticlePanier(string idPanier, string idArticle)
        {
            int idPan = int.Parse(idPanier);
            int idArt = int.Parse(idArticle);
            var remove =
                (from p in context.Panier
                where p.IdPanier == idPan && p.IdArticle == idArt
                select p).ToList();
            if(remove != null)
            {
                Panier delPanier = remove[0];
                context.Panier.Remove(delPanier);
                context.SaveChanges();
            }
            
        }
    }
}

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
    public class ArticleController : ControllerBase
    {
        private SupermarcheContext context;
        public ArticleController(SupermarcheContext _context )
        {
            context = _context;
        }

        [HttpGet("allArticle")]
        public string AllArticle()
        {
            var allArticle =
                from a in context.Article
                select new { a.Id, a.Nom, a.Quantite, a.Prix, a.IdCategorieNavigation.Label};
            return JsonConvert.SerializeObject(allArticle);
        }

        [HttpGet("byCategoryArticle/{idCategory}")]
        public string ByCategoryArticle(string idCategory = "1")
        {
            int idCat = int.Parse(idCategory);
            var allArticle =
                (from a in context.Article
                 where a.IdCategorie == idCat
                 select new { a.Nom, a.Quantite, a.Prix, a.IdCategorieNavigation.Label });
            return JsonConvert.SerializeObject(allArticle);
        }

        [HttpGet("quantiteArticle/{idArticle}")]
        public string QuantiteArticle(string idArticle)
        {
            int idArt = int.Parse(idArticle);
            var quantiteArticle =
                (from a in context.Article
                 where a.Id == idArt
                 select new { a.Quantite });
            return JsonConvert.SerializeObject(quantiteArticle);
        }

        [HttpPost("achatArticle")]
        public void AchatArticle([FromBody] ArticlePanier article)
        {
            int idArt = article.Id;
            int idCli = (int)article.IdClient;

            var maxCliPanier =
                ((from p in context.Panier
                  where p.IdClient == idCli
                  select p).Max(p => p.IdPanier));

            if (maxCliPanier < 1)
            {
                maxCliPanier = 1;
            }
            var articlePanier =
                (from p in context.Panier
                 where p.IdArticle == idArt && p.IdClient == idCli && p.IdPanier == maxCliPanier
                 select new { p.IdPanier, p.IdClient, p.IdArticle, p.Quantite, p.DateAchat, QuantiteArticle = p.IdArticleNavigation.Quantite, p.IdArticleNavigation.Prix, p.IdClientNavigation.Argent }).ToList();

            if (articlePanier != null && articlePanier.Count == 1)
            {
                //Si on a trouvé l'article mais que le panier a déjà été acheté on créé un nouveau panier avec l'article
                if (articlePanier[0].DateAchat != null)
                {
                    maxCliPanier += 1;
                    int newQuantite = (int)article.Quantite;
                    Panier newPanier = new Panier() { IdPanier = maxCliPanier, IdClient = articlePanier[0].IdClient, IdArticle = articlePanier[0].IdArticle, Quantite = newQuantite, DateAchat = null };
                    context.Panier.Add(newPanier);
                }
                //Si on a trouvé l'article et qu'il existe déjà le panier on met à jour l'article du panier
                else
                {
                    int newQuantite = articlePanier[0].Quantite + (int)article.Quantite;
                    Panier updPanier = new Panier() { IdPanier = articlePanier[0].IdPanier, IdClient = articlePanier[0].IdClient, IdArticle = articlePanier[0].IdArticle, Quantite = newQuantite, DateAchat = articlePanier[0].DateAchat };
                    context.Panier.Update(updPanier);
                }

            }
            //S'il n'existe pas l'article dans le panier actuelle, on ajoute l'article
            else if (articlePanier.Count == 0)
            {
                Panier newArticlePanier = new Panier() { IdPanier = maxCliPanier, IdClient = idCli, IdArticle = idArt, Quantite = (int)article.Quantite, DateAchat = null };
                context.Panier.Add(newArticlePanier);
            }
            context.SaveChanges();
        }


        /*[HttpPost("addArticle")]
        public void AddArticle(Article article)
        {
            context.Article.Add(article);
            context.SaveChanges();
        }*/
    }
}

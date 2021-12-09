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
    public class ClientController : ControllerBase
    {
        private SupermarcheContext context;
        public ClientController(SupermarcheContext _context )
        {
            context = _context;
        }

        [HttpGet("allClient")]
        public string AllClient()
        {
            //var GroupByPanier = context.Panier.GroupBy(p => p.IdPanier);
            var allClient =
                from c in context.Client
                select new { c.Id, c.Nom, c.Prenom, c.Argent };
            return JsonConvert.SerializeObject(allClient);
        }

        [HttpGet("byClient/{idClient}")]
        public string ByClient(string idClient)
        {
            int idCli = int.Parse(idClient);
            var client =
                from c in context.Client
                 where c.Id == idCli
                 select new { c.Nom, c.Prenom };
            return JsonConvert.SerializeObject(client);
        }

        /*[HttpPost("addClient")]
        public void AddClient(Client client)
        {
            context.Client.Add(client);
            context.SaveChanges();
        }*/
    }
}

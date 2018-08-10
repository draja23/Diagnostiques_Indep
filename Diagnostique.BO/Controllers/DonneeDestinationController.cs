using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diagnostique.BO.ModelDestination;
using Diagnostique.BO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diagnostique.BO.Controllers
{
    /// <summary>
    /// Par Rajaram.D Le 04/07/2018
    /// API pour Ajout est sélection des données...
    /// </summary>
    [Route("api/[controller]")]
    public class DonneeDestinationController : Controller
    {
        //Logger
        readonly ILogger<DonneeDestinationController> _logDDC;
        //Accés aux données
        private readonly IDestinationDataAcessLayer _destinationDataAcessLayer;

        public DonneeDestinationController(ILogger<DonneeDestinationController> logDDC, IDestinationDataAcessLayer destinationDataAcessLayer)
        {
            _logDDC = logDDC;
            _destinationDataAcessLayer = destinationDataAcessLayer;
        }

        /// <summary>
        /// Action pour ajout pour des données...
        /// </summary>
        /// <param name="srcDonnee">Récupére une liste [FromBody] List<SourceDonnees</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        // POST api/<controller>
        [HttpPost("[action]")]
        public bool AjoutDonneesDIT([FromBody] List<SourceFormarteDonnees> srcDonnee)
        {
            if (srcDonnee.Count == 0)
                return false;

            try
            {
                //DestinationDataAcessLayer dalIn = new DestinationDataAcessLayer(); //DAL OBJECT
                return _destinationDataAcessLayer.InsertToDBB(srcDonnee); //INVOKE METHOD
            }
            catch (Exception ex)
            {
                _logDDC.LogInformation("DonneeDestinationController : InnerException = {0} - Message {1}", ex.InnerException, ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Action pour sélection des données...
        /// </summary>
        /// <param name="premierDateCom">Filtre de date de première commande</param>   
        /// <param name="derniereDateCom">Filtre de date de dernière commande</param> 
        /// <param name="premierFactureDateCom">Filtre de date de premiére facture</param>  
        /// <param name="dernierFactureDateCom">Filtre de date de derniére facture</param>  
        /// <param name="nom">Filtre nom</param>  
        /// <param name="prenom">Filtre prénom</param>  
        /// <param name="secteur">Filtre secteur</param>  
        /// <returns>Retourne une liste données en filtrant</returns>
        [HttpGet("[action]")]
        public IQueryable<SourceFormarteDonnees> SelectionDonneesDIT(string premierDateCom, string derniereDateCom, string premierFactureDateCom, string dernierFactureDateCom, string nom, string prenom, string secteur)
        {
            try
            {
                bool datePreComBl = DateTime.TryParse(premierDateCom, out DateTime datePreCom);
                bool dateDerComBl = DateTime.TryParse(derniereDateCom, out DateTime dateDerCom);
                bool datePremFactComBl = DateTime.TryParse(premierFactureDateCom, out DateTime datePremFactCom);
                bool dateDernFactComBl = DateTime.TryParse(dernierFactureDateCom, out DateTime dateDernFactCom);

                //DestinationDataAcessLayer dalOut = new DestinationDataAcessLayer(); //DAL OBJECT
                return _destinationDataAcessLayer.GetAllItemsFromDBBFiltered(datePreCom, dateDerCom, datePremFactCom, dateDernFactCom, nom, prenom, secteur); //INVOKE METHOD
            }
            catch (Exception ex)
            {
                _logDDC.LogInformation("DonneeDestinationController : InnerException = {0} - Message {1}", ex.InnerException, ex.Message);
                return null;
            }
        }
    }
}
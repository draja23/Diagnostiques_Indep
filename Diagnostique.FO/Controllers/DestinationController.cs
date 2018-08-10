using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diagnostique.FO.Controllers
{
    /// <summary>
    /// Par Rajaram.D Le 04/07/2018
    /// API pour Ajout est sélection des données...
    /// </summary>
    [Route("api/[controller]")]
    public class DestinationFoController : Controller
    {
        //Logger
        readonly ILogger<DestinationFoController> _logDDC;

        public DestinationFoController(ILogger<DestinationFoController> logDDC)
        {
            _logDDC = logDDC;
        }

        /// <summary>
        /// Action pour ajout pour des données...
        /// </summary>
        /// <param name="srcDonnee">Récupére une liste [FromBody] List<SourceDonnees</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        //POST api/<controller>
        [HttpPost("[action]")]
        public async Task<bool> AjoutDonneesDITAsync([FromBody] List<dynamic> srcDonnee)
        {
            if (srcDonnee.Count == 0)
                return false;

            try
            {   
                ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper();
                return await apiHelper.PostApiAsync<Boolean, List<dynamic>>($"api/DonneeDestination/AjoutDonneesDIT", srcDonnee);
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
        public async Task<List<dynamic>> SelectionDonneesDITAsync(string premierDateCom, string derniereDateCom, string premierFactureDateCom, string dernierFactureDateCom, string nom, string prenom, string secteur)
        {
            try
            {
                bool datePreComBl = DateTime.TryParse(premierDateCom, out DateTime datePreCom);
                bool dateDerComBl = DateTime.TryParse(derniereDateCom, out DateTime dateDerCom);
                bool datePremFactComBl = DateTime.TryParse(premierFactureDateCom, out DateTime datePremFactCom);
                bool dateDernFactComBl = DateTime.TryParse(dernierFactureDateCom, out DateTime dateDernFactCom);

                //Récupération des données en filtrant.             
                ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper();
                return await apiHelper.GetApiAsync<List<dynamic>>($"api/DonneeDestination/SelectionDonneesDIT?premierDateCom={premierDateCom}&derniereDateCom={derniereDateCom}&premierFactureDateCom={premierFactureDateCom}&dernierFactureDateCom={dernierFactureDateCom}&nom={nom}&prenom={prenom}&secteur={secteur}");
            }
            catch (Exception ex)
            {
                _logDDC.LogInformation("DonneeDestinationController : InnerException = {0} - Message {1}", ex.InnerException, ex.Message);
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diagnostique.FO.Controllers
{
    /// <summary>
    /// Par Rajaram.D Le 04/07/2018
    /// API pour sélection des données à partir d'une base de source...
    /// </summary>
    [Route("api/[controller]")]
    public class SourceFoController : Controller
    {
        //DAL OBJECT
        // SourceDataAccessLayer sdal = new SourceDataAccessLayer();
        //event Logger (errors, messages etc..)
        readonly ILogger<SourceFoController> _logSDAL;

        public SourceFoController(ILogger<SourceFoController> logsDAL)
        {
            _logSDAL = logsDAL;
        }

        /// <summary>
        /// Action pour récupérer les données de source...
        /// </summary>
        /// <param name="premierDateCom">Filtre de date de première commande</param>   
        /// <param name="derniereDateCom">Filtre de date de dernière commande</param> 
        /// <param name="premierFactureDateCom">Filtre de date de premiére facture</param>  
        /// <param name="dernierFactureDateCom">Filtre de date de derniére facture</param>  
        /// <param name="nom">Filtre nom</param>  
        /// <param name="prenom">Filtre prénom</param>  
        /// <param name="secteur">Filtre secteur</param>  
        /// <returns>Retourne une liste données en filtrant</returns>
        // GET api/<controller>
        [HttpGet("[action]")]
        public async Task<List<dynamic>> SelectionDonneesSourceAsync(string premierDateCom, string derniereDateCom, string premierFactureDateCom, string dernierFactureDateCom, string nom, string prenom, string secteur)
        {
           
            try
            {
                bool datePreComBl = DateTime.TryParse(premierDateCom, out DateTime datePreCom);
                bool dateDerComBl = DateTime.TryParse(derniereDateCom, out DateTime dateDerCom);
                bool datePremFactComBl = DateTime.TryParse(premierFactureDateCom, out DateTime datePremFactCom);
                bool dateDernFactComBl = DateTime.TryParse(dernierFactureDateCom, out DateTime dateDernFactCom);

                //Récupération des données en filtrant.             
                ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper();
                return await apiHelper.GetApiAsync<List<dynamic>>($"api/DonneeSource/SelectionDonneesSource?premierDateCom={datePreCom}&derniereDateCom={dateDerCom}&premierFactureDateCom={datePremFactCom}&dernierFactureDateCom={dateDernFactCom}&nom={nom}&prenom={prenom}&secteur={secteur}");
                
            }
            catch (Exception ex)
            {
                _logSDAL.LogInformation("DonneeSourceController : InnerException = {0} - Message {1}", ex.InnerException, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Méthode pour mise en forme des noms, lignes et indexes des tables...
        /// </summary>
        /// <param name="donneeBrut">chaine de caractéres avec ;</param>           
        /// <returns>Retourne une liste List<IndexDetails> </returns>
        private List<dynamic> IndDet(string donneeBrut)
        {
            List<dynamic> returnData = new List<dynamic>();
            string[] extract = donneeBrut.Split(";");

            foreach (string chaqueExtract in extract)
            {
                returnData.Add(new
                {
                    TableNom = chaqueExtract.Split(":")[0],
                    Indexes = chaqueExtract.Split(":")[1].Split(",")
                });
            }
            return returnData;
        }
    }
}
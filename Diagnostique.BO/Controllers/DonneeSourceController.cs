using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diagnostique.BO.Models;
using Diagnostique.BO.ModelSource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diagnostique.BO.Controllers
{
    /// <summary>
    /// Par Rajaram.D Le 04/07/2018
    /// API pour sélection des données à partir d'une base de source...
    /// </summary>
    [Route("api/[controller]")]
    public class DonneeSourceController : Controller
    {
        //DAL OBJECT
        // SourceDataAccessLayer sdal = new SourceDataAccessLayer();
        //event Logger (errors, messages etc..)
        readonly ILogger<DonneeSourceController> _logSDAL;
        //Accés aux données
        private readonly ISourceDataAccessLayer _sourceDataAccessLayer;

        public DonneeSourceController(ILogger<DonneeSourceController> logsDAL, ISourceDataAccessLayer sourceDataAccessLayer)
        {
            _logSDAL = logsDAL;
            _sourceDataAccessLayer = sourceDataAccessLayer;
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
        public List<SourceFormarteDonnees> SelectionDonneesSource(string premierDateCom, string derniereDateCom, string premierFactureDateCom, string dernierFactureDateCom, string nom, string prenom, string secteur)
        {
            //Liste à retourner
            List<SourceFormarteDonnees> donRetList = new List<SourceFormarteDonnees>();
            try
            {
                bool datePreComBl = DateTime.TryParse(premierDateCom, out DateTime datePreCom);
                bool dateDerComBl = DateTime.TryParse(derniereDateCom, out DateTime dateDerCom);
                bool datePremFactComBl = DateTime.TryParse(premierFactureDateCom, out DateTime datePremFactCom);
                bool dateDernFactComBl = DateTime.TryParse(dernierFactureDateCom, out DateTime dateDernFactCom);

                //Récupération des données en filtrant.
                IQueryable<DonneeSource> donneesBrut = _sourceDataAccessLayer.GetAllSourcesFromBDD(datePreCom, dateDerCom, datePremFactCom, dateDernFactCom, nom, prenom, secteur);

                //itération de chaque élement de la liste pour mise en forme.               
                if (donneesBrut.Count() > 0)
                {
                    foreach (DonneeSource chaqueDonne in donneesBrut)
                    {
                        donRetList.Add(new SourceFormarteDonnees
                        {
                            ColId = chaqueDonne.ColId,
                            SourceFormarteUtilNom = chaqueDonne.SourceUtilNom,
                            SourceFormarteUtilPrenom = chaqueDonne.SourceUtilPrenom,
                            SourceFormarteSectNom = chaqueDonne.SourceSectNom,
                            SourceFormarteCatalPrincipal = chaqueDonne.SourceCatalPrincipal,
                            SourceFormarteCatalAutrePrincipal = chaqueDonne.SourceCatalAutrePrincipal,
                            SourceFormarteVersNom = chaqueDonne.SourceVersNom,
                            SourceFormarteComPremierDateId = chaqueDonne.SourceComPremierDateId,
                            SourceFormarteComPremierDate = chaqueDonne.SourceComPremierDate,
                            SourceFormarteComDernierDateId = chaqueDonne.SourceComDernierDateId,
                            SourceFormarteComDerniereDate = chaqueDonne.SourceComDerniereDate,
                            SourceFormarteComPremierFactureDateId = chaqueDonne.SourceComPremierFactureDateId,
                            SourceFormarteComPremierFactureDate = chaqueDonne.SourceComPremierFactureDate,
                            SourceFormarteComDernierFactureDateId = chaqueDonne.SourceComDernierFactureDateId,
                            SourceFormarteComDerniereFactureDate = chaqueDonne.SourceComDerniereFactureDate,
                            SourceFormarteVisDate = chaqueDonne.SourceVisDate,
                            SourceFormarteTabNom = chaqueDonne.SourceTabNom.Split(";"),
                            SourceFormarteTabLigneComptage = chaqueDonne.SourceTabLigneComptage.Split(";"),
                            SourceFormarteTabNomIndex = IndDet(chaqueDonne.SourceTabNomIndex)
                        });
                    }
                }
                else
                {
                    donRetList = null;
                }
            }
            catch (Exception ex)
            {
                _logSDAL.LogInformation("DonneeSourceController : InnerException = {0} - Message {1}", ex.InnerException, ex.Message);
                donRetList = null;
            }

            return donRetList;
        }

        /// <summary>
        /// Méthode pour mise en forme des noms, lignes et indexes des tables...
        /// </summary>
        /// <param name="donneeBrut">chaine de caractéres avec ;</param>           
        /// <returns>Retourne une liste List<IndexDetails> </returns>
        private List<IndexDetails> IndDet(string donneeBrut)
        {
            List<IndexDetails> returnData = new List<IndexDetails>();
            string[] extract = donneeBrut.Split(";");

            foreach (string chaqueExtract in extract)
            {
                returnData.Add(new IndexDetails
                {
                    TableNom = chaqueExtract.Split(":")[0],
                    Indexes = chaqueExtract.Split(":")[1].Split(",")
                });
            }
            return returnData;
        }
    }
}
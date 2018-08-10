using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diagnostique.BO.Models;

namespace Diagnostique.BO.ModelSource
{
    /// <summary>
    /// Par Rajaram.D Le 03/07/2018
    /// La classe pour récupérer des données qui récupére à partir de la source...
    /// </summary>
    public class SourceDataAccessLayer : ISourceDataAccessLayer
    {
        //Contexte est utilisé pour l'opération de sélection de source.
        DiagnoInfoTerrainSourceContext sourceDAL = new DiagnoInfoTerrainSourceContext();

        /// <summary>
        /// Méthode pour séléctionner les données à partir de la base de source,
        /// </summary>
        /// <param name="premierDateCom">Filtre de date de première commande</param>   
        /// <param name="derniereDateCom">Filtre de date de dernière commande</param> 
        /// <param name="premierFactureDateCom">Filtre de date de premiére facture</param>  
        /// <param name="dernierFactureDateCom">Filtre de date de derniére facture</param>  
        /// <param name="nom">Filtre nom</param>  
        /// <param name="prenom">Filtre prénom</param>  
        /// <param name="secteur">Filtre secteur</param>  
        /// <returns>Retourne une liste d'élément en filtrant</returns>
        public IQueryable<DonneeSource> GetAllSourcesFromBDD(DateTime premierDateCom, DateTime derniereDateCom, DateTime premierFactureDateCom, DateTime dernierFactureDateCom, string nom, string prenom, string secteur)
        {
            try
            {
                //Récupération des données.
                //IEnumerable<DonneeSource> RawDs = sourceDAL.Donneesource
                //                                               .Where(ic => ic.EstCopieDestination == false)
                //                                               .Where(s => premierDateCom != DateTime.MinValue ? s.SourceComPremierDate.Value.Date == premierDateCom.Date : false)
                //                                               .Where(s => derniereDateCom != DateTime.MinValue && s.SourceComDerniereDate.Value.Date == derniereDateCom.Date)
                //                                               .Where(s => premierFactureDateCom != DateTime.MinValue && s.SourceComPremierFactureDate.Value.Date == premierFactureDateCom.Date)
                //                                               .Where(s => dernierFactureDateCom != DateTime.MinValue && s.SourceComDerniereFactureDate.Value.Date == dernierFactureDateCom.Date)
                //                                               .Where(s => nom != string.Empty && s.SourceUtilNom == nom)
                //                                               .Where(s => prenom != string.Empty && s.SourceUtilPrenom == prenom)
                //                                               .Where(s => secteur != string.Empty && s.SourceSectNom == secteur)
                //                                           .ToList();

                //Récupération des données.
                IQueryable<DonneeSource> donneesBrut = sourceDAL.Donneesource.Where(ic => ic.EstCopieDestination == false);
                //Filtre
                if (premierDateCom != DateTime.MinValue)
                    donneesBrut = donneesBrut.Where(x => x.SourceComPremierDate.Value.ToShortDateString() == premierDateCom.ToShortDateString());
                //Filtre
                if (derniereDateCom != DateTime.MinValue)
                    donneesBrut = donneesBrut.Where(x => x.SourceComDerniereDate.Value.ToShortDateString() == derniereDateCom.ToShortDateString());
                //Filtre
                if (premierFactureDateCom != DateTime.MinValue)
                    donneesBrut = donneesBrut.Where(x => x.SourceComPremierFactureDate.Value.ToShortDateString() == premierFactureDateCom.ToShortDateString());
                //Filtre
                if (dernierFactureDateCom != DateTime.MinValue)
                    donneesBrut = donneesBrut.Where(x => x.SourceComDerniereFactureDate.Value.ToShortDateString() == dernierFactureDateCom.ToShortDateString());
                //Filtre
                if (nom != null)
                    donneesBrut = donneesBrut.Where(x => x.SourceUtilNom == nom);
                //Filtre
                if (prenom != null)
                    donneesBrut = donneesBrut.Where(x => x.SourceUtilPrenom == prenom);
                //Filtre
                if (secteur != null)
                    donneesBrut = donneesBrut.Where(x => x.SourceSectNom == secteur);

                return donneesBrut;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Méthode pour mettre à jour la colonne dans la table sourcedonnee...
        /// </summary>
        /// <param name="updateCopiedColumn">Qui contient la colonne à mettre à jour 'ColId'</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        public bool UpdateSourceCopie(SourceFormarteDonnees updateCopiedColumn)
        {
            try
            {
                //selection d'objet à mettre à jour....
                DonneeSource sourceToUpdate = sourceDAL.Donneesource.Where(idc => idc.ColId == updateCopiedColumn.ColId).FirstOrDefault();
                //collonne à mettre à jour 
                if (sourceToUpdate != null)
                {
                    sourceToUpdate.EstCopieDestination = true;
                    sourceDAL.SaveChanges();
                    //if update is ok.
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface ISourceDataAccessLayer
    {
        IQueryable<DonneeSource> GetAllSourcesFromBDD(DateTime premierDateCom, DateTime derniereDateCom, DateTime premierFactureDateCom, DateTime dernierFactureDateCom, string nom, string prenom, string secteur);
        bool UpdateSourceCopie(SourceFormarteDonnees updateCopiedColumn);
    }
}

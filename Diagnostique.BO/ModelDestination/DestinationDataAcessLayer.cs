using Diagnostique.BO.Models;
using Diagnostique.BO.ModelSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diagnostique.BO.ModelDestination
{
    /// <summary>
    /// Par Rajaram.D Le 03/07/2018
    /// La classe pour l'insertion des données qui récupére à partir de la source...
    /// </summary>
    public class DestinationDataAcessLayer : IDestinationDataAcessLayer
    {
        //Contexte utiliser pour l'opération de l'insertion.
        DiagnoInfoTerrainContext destinationDAL = new DiagnoInfoTerrainContext();

        /// <summary>
        /// Méthode pour insertion des données qui reçoit une liste de données...
        /// </summary>
        /// <param name="source">liste des données</param> 
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        public Boolean InsertToDBB(List<SourceFormarteDonnees> source)
        {
            foreach (SourceFormarteDonnees srcToDestination in source)
            {
                bool isTransactionOk = true;
                using (var transaction = destinationDAL.Database.BeginTransaction()) // Pour faire le ROLLBACK.
                {
                    int usrId = 0;

                    try
                    {
                        //Insertion utilisateur.
                        usrId = InsertUser(srcToDestination); //Enregistrement obligatoire dans la BDD car besoin d'id.
                        srcToDestination.SourceFormarteUtilId = usrId; //affectation d'id sur la source.

                        //si id utilisateur est 0, retourne false.
                        if (usrId == 0)
                            throw new InvalidOperationException("ID NE PEUT PAS ETRE 0 !!!"); // direction à la prochaine catch...

                        //Insertion table et index.
                        //Enregistrement obligatoire (nom table) dans la BDD car besoin d'id pour l'insertion des noms index avec l'id de la table.
                        bool isOkTabInd = InsertTableLigneIndex(srcToDestination);
                        //Insertion secteur.
                        bool isOkSect = InsertSecteur(srcToDestination);
                        //Insertion Catalogue.
                        bool isOkCat = InsertCatalogue(srcToDestination);
                        //Insertion Catalogue autre.
                        bool isOkACat = InsertAutreCatalogue(srcToDestination);
                        //Insertion Version.
                        bool isOkVer = InsertVersion(srcToDestination);
                        //Insertion visite.
                        bool isOkVis = InsertVisite(srcToDestination);
                        //Insertion Commande.
                        bool isOkCom = InsertCommande(srcToDestination);

                        //Si tous les set sont mises à jour correctement...Insertion dans la base.
                        if (isOkSect && isOkCat && isOkACat && isOkVer && isOkVis && isOkCom && isOkTabInd)
                        {
                            try
                            {
                                destinationDAL.SaveChanges();
                            }
                            catch
                            {
                                isTransactionOk = false;
                            }
                            //Mettre à jour la table souceDonne, la colonne 'est_copie_destination'
                            if (isTransactionOk)
                                UpdateSourceCopieDestination(srcToDestination);
                        }
                        else
                        {
                            isTransactionOk = false;
                        }
                    }
                    catch
                    {
                        isTransactionOk = false;
                    }
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    if (isTransactionOk)
                        transaction.Commit();
                }
            }
            return true;
        }

        /// <summary>
        /// Méthode pour insérer un utilisateur...
        /// </summary>
        /// <param name="user">liste des données, récupere seulement les éléments utilisateur (SourceFormarteDonnees)</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private int InsertUser(SourceFormarteDonnees user)
        {
            try
            {
                //Create user object....
                Utilisateur newUser = new Utilisateur();
                newUser.SourceId = user.ColId;
                newUser.UtilNom = user.SourceFormarteUtilNom;
                newUser.UtilPrenom = user.SourceFormarteUtilPrenom;

                //add objet data to a table...
                destinationDAL.Utilisateur.Add(newUser);
                destinationDAL.SaveChanges(); //Insertion dans la base est obligatoire car l'id d'utilisateur est requis.
                //return generated Id
                return newUser.UtilId;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Méthode pour insérer le secteur...
        /// </summary>
        /// <param name="secteur">liste des données, récupere seulement les éléments de secteur (SourceFormarteDonnees)</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool InsertSecteur(SourceFormarteDonnees secteur)
        {
            try
            {
                //Create secteur object....
                Secteur newUserSect = new Secteur();
                newUserSect.IdUtil = secteur.SourceFormarteUtilId;
                newUserSect.SectNom = secteur.SourceFormarteSectNom;

                //add objet data to a table...
                destinationDAL.Secteur.Add(newUserSect);

                //if insertion is ok.
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode pour insérer un catalogue...
        /// </summary>
        /// <param name="catal">liste des données, récupere seulement les éléments de catalogue  (SourceFormarteDonnees)</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool InsertCatalogue(SourceFormarteDonnees catal)
        {
            try
            {
                //Create catalogue object....
                Catalogue newCatalSect = new Catalogue();
                newCatalSect.IdUtil = catal.SourceFormarteUtilId;
                newCatalSect.CatalPrincipal = catal.SourceFormarteCatalPrincipal;

                //add objet data to a table...
                destinationDAL.Catalogue.Add(newCatalSect);

                //if insertion is ok.
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode pour insérer le autre catalogue...
        /// </summary>
        /// <param name="catalAutre">liste des données, récupere seulement les éléments d'autre catalogue  (SourceFormarteDonnees)</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool InsertAutreCatalogue(SourceFormarteDonnees catalAutre)
        {
            try
            {
                //Create autre catalogue object....
                CatalogueAutre newCatalAutre = new CatalogueAutre();
                newCatalAutre.IdUtil = catalAutre.SourceFormarteUtilId;
                newCatalAutre.CatalAutrePrincipal = catalAutre.SourceFormarteCatalAutrePrincipal;

                //add objet data to a table...
                destinationDAL.CatalogueAutre.Add(newCatalAutre);

                //if insertion is ok.
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode pour insérer la version...
        /// </summary>
        /// <param name="vers">liste des données, récupére seulement les éléments de la version (SourceFormarteDonnees)</param>            
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool InsertVersion(SourceFormarteDonnees vers)
        {
            try
            {
                //Create autre catalogue object....
                Version newVers = new Version();
                newVers.IdUtil = vers.SourceFormarteUtilId;
                newVers.VersNom = vers.SourceFormarteVersNom;

                //add  objet data to a table...
                destinationDAL.Version.Add(newVers);

                //if insertion is ok.
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode pour insérer une date de visite...
        /// </summary>
        /// <param name="visite">liste des données, récupére seulement les éléments d'une visite (SourceFormarteDonnees)</param>            
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool InsertVisite(SourceFormarteDonnees visite)
        {
            try
            {
                //Create autre catalogue object....
                Visite newVisite = new Visite();
                newVisite.IdUtil = visite.SourceFormarteUtilId;
                newVisite.VisDate = visite.SourceFormarteVisDate;

                //add  objet data to a table...
                destinationDAL.Visite.Add(newVisite);

                //if insertion is ok.
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode pour insérer une commande...
        /// </summary>
        /// <param name="commande">liste des données, récupére seulement les éléments d'une commande (SourceFormarteDonnees)</param>             
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool InsertCommande(SourceFormarteDonnees commande)
        {
            try
            {
                //Create COMMANDE object....
                Commande newCommande = new Commande();
                newCommande.IdUtil = commande.SourceFormarteUtilId;
                newCommande.ComPremierDateId = commande.SourceFormarteComPremierDateId;
                newCommande.ComPremierDate = commande.SourceFormarteComPremierDate;
                newCommande.ComDernierDateId = commande.SourceFormarteComDernierDateId;
                newCommande.ComDerniereDate = commande.SourceFormarteComDerniereDate;
                newCommande.ComPremierFactureDateId = commande.SourceFormarteComPremierFactureDateId;
                newCommande.ComPremierFactureDate = commande.SourceFormarteComPremierFactureDate;
                newCommande.ComDernierFactureDateId = commande.SourceFormarteComDernierFactureDateId;
                newCommande.ComDerniereFactureDate = commande.SourceFormarteComDerniereFactureDate;

                //add COMMANDE objet data to a table...
                destinationDAL.Commande.Add(newCommande);

                //if insertion is ok.
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode pour insérer les noms des tables et les index associés...
        /// </summary>
        /// <param name="tableEtLigne">liste des données, récupére seulement les éléments des tables/indexes (SourceFormarteDonnees)</param>            
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool InsertTableLigneIndex(SourceFormarteDonnees tableEtLigne)
        {
            try
            {
                //ajout nom table et ligne.                
                for (int item = 0; item < tableEtLigne.SourceFormarteTabNom.Count(); item++)
                {
                    try
                    {
                        TableNomComptage newTableWithComptage = new TableNomComptage();
                        newTableWithComptage.IdUtil = tableEtLigne.SourceFormarteUtilId;
                        newTableWithComptage.TabNom = tableEtLigne.SourceFormarteTabNom[item].Trim();
                        newTableWithComptage.TabLigneComptage = Convert.ToInt32(tableEtLigne.SourceFormarteTabLigneComptage[item].Trim());
                        destinationDAL.TableNomComptage.Add(newTableWithComptage);
                        destinationDAL.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                }
                //ajout index par table.              
                foreach (IndexDetails tabIndex in tableEtLigne.SourceFormarteTabNomIndex)
                {
                    var tabledetails = destinationDAL.TableNomComptage.Where(nom => nom.TabNom == tabIndex.TableNom && nom.IdUtil == tableEtLigne.SourceFormarteUtilId).FirstOrDefault();

                    if (tabledetails != null)
                    {
                        foreach (string idx in tabIndex.Indexes)
                        {
                            try
                            {
                                TableIndex newTableIndexes = new TableIndex();
                                newTableIndexes.IdNcTable = tabledetails.TableNcId;
                                newTableIndexes.TabNomIndex = idx.Trim();
                                destinationDAL.TableIndex.Add(newTableIndexes);
                            }
                            catch
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode pour mettre à jour la colonne de la table Donneesource...
        /// </summary>
        /// <param name="SourceDonnees">liste des données, récupére seulement les éléments d'une commande</param>           
        /// <returns>retourne un booléan true/false, true si l'opération OK si non false</returns>
        private bool UpdateSourceCopieDestination(SourceFormarteDonnees estCopie)
        {
            //DAL OBJECT
            SourceDataAccessLayer sDal = new SourceDataAccessLayer();
            try
            {
                return sDal.UpdateSourceCopie(estCopie);
            }
            catch
            {
                return false;
            }
        }




        /// <summary>
        /// Méthode pour séléctionner les données des tables en les filtrant,
        /// </summary>
        /// <param name="premierDateCom">Filtre de date de première commande</param>   
        /// <param name="derniereDateCom">Filtre de date de dernière commande</param> 
        /// <param name="premierFactureDateCom">Filtre de date de premiére facture</param>  
        /// <param name="dernierFactureDateCom">Filtre de date de derniére facture</param>  
        /// <param name="nom">Filtre nom</param>  
        /// <param name="prenom">Filtre prénom</param>  
        /// <param name="secteur">Filtre secteur</param>  
        /// <returns>Retourne tous les table sous forme d'une liste en filtrant</returns>
        public IQueryable<SourceFormarteDonnees> GetAllItemsFromDBBFiltered(DateTime premierDateCom, DateTime derniereDateCom, DateTime premierFactureDateCom, DateTime dernierFactureDateCom, String nom, String prenom, String secteur)
        {
            IQueryable<SourceFormarteDonnees> filteredListToReturn;
            try
            {
                //Récupération des données.
                filteredListToReturn = GetAllItemsFromDBB();

                //Filtre
                if (premierDateCom != DateTime.MinValue)
                    filteredListToReturn = filteredListToReturn.Where(x => x.SourceFormarteComPremierDate.Value.ToShortDateString() == premierDateCom.ToShortDateString());
                //Filtre
                if (derniereDateCom != DateTime.MinValue)
                    filteredListToReturn = filteredListToReturn.Where(x => x.SourceFormarteComDerniereDate.Value.ToShortDateString() == derniereDateCom.ToShortDateString());
                //Filtre
                if (premierFactureDateCom != DateTime.MinValue)
                    filteredListToReturn = filteredListToReturn.Where(x => x.SourceFormarteComPremierFactureDate.Value.ToShortDateString() == premierFactureDateCom.ToShortDateString());
                //Filtre
                if (dernierFactureDateCom != DateTime.MinValue)
                    filteredListToReturn = filteredListToReturn.Where(x => x.SourceFormarteComDerniereFactureDate.Value.ToShortDateString() == dernierFactureDateCom.ToShortDateString());
                //Filtre
                if (nom != null)
                    filteredListToReturn = filteredListToReturn.Where(x => x.SourceFormarteUtilNom == nom);
                //Filtre
                if (prenom != null)
                    filteredListToReturn = filteredListToReturn.Where(x => x.SourceFormarteUtilPrenom == prenom);
                //Filtre
                if (secteur != null)
                    filteredListToReturn = filteredListToReturn.Where(x => x.SourceFormarteSectNom == secteur);
            }
            catch (Exception)
            {
                return null;
            }
            return filteredListToReturn;
        }

        /// <summary>
        /// Méthode pour séléctionner les données des tables,
        /// catalogue,catalogue_autre,commande,secteur,table_index,table_nom_comptage,utilisateur,version,visite
        /// </summary>
        /// <param name=""></param>   
        /// <returns>Retourne tous les table sous forme d'une liste </returns>
        private IQueryable<SourceFormarteDonnees> GetAllItemsFromDBB()
        {
            try
            {
                //Sélection des élements des tables...
                IQueryable<SourceFormarteDonnees> listToReturn = from usr in destinationDAL.Utilisateur
                                                                 join sec in destinationDAL.Secteur on usr.UtilId equals sec.IdUtil
                                                                 join cat in destinationDAL.Catalogue on usr.UtilId equals cat.IdUtil
                                                                 join catAut in destinationDAL.CatalogueAutre on usr.UtilId equals catAut.IdUtil
                                                                 join ver in destinationDAL.Version on usr.UtilId equals ver.IdUtil
                                                                 join vis in destinationDAL.Visite on usr.UtilId equals vis.IdUtil
                                                                 join com in destinationDAL.Commande on usr.UtilId equals com.IdUtil
                                                                 select new SourceFormarteDonnees
                                                                 {
                                                                     ColId = usr.SourceId,
                                                                     SourceFormarteUtilId = usr.UtilId,
                                                                     SourceFormarteUtilNom = usr.UtilNom,
                                                                     SourceFormarteUtilPrenom = usr.UtilPrenom,
                                                                     SourceFormarteSectNom = sec.SectNom,
                                                                     SourceFormarteCatalPrincipal = cat.CatalPrincipal,
                                                                     SourceFormarteCatalAutrePrincipal = catAut.CatalAutrePrincipal,
                                                                     SourceFormarteVersNom = ver.VersNom,
                                                                     SourceFormarteComPremierDateId = com.ComPremierDateId,
                                                                     SourceFormarteComPremierDate = com.ComPremierDate,
                                                                     SourceFormarteComDernierDateId = com.ComDernierDateId,
                                                                     SourceFormarteComDerniereDate = com.ComDerniereDate,
                                                                     SourceFormarteComPremierFactureDateId = com.ComPremierFactureDateId,
                                                                     SourceFormarteComPremierFactureDate = com.ComPremierFactureDate,
                                                                     SourceFormarteComDernierFactureDateId = com.ComDernierFactureDateId,
                                                                     SourceFormarteComDerniereFactureDate = com.ComDerniereFactureDate,
                                                                     SourceFormarteVisDate = vis.VisDate,
                                                                     SourceFormarteTabNom = destinationDAL.TableNomComptage.Where(ui => ui.IdUtil == usr.UtilId).Select(tn => tn.TabNom).ToArray(),
                                                                     SourceFormarteTabLigneComptage = destinationDAL.TableNomComptage.Where(ui => ui.IdUtil == usr.UtilId).Select(tn => tn.TabLigneComptage.ToString()).ToArray(),
                                                                     SourceFormarteTabNomIndex = destinationDAL.TableNomComptage.Where(ui => ui.IdUtil == usr.UtilId)
                                                                                                                    .Select(tn => new IndexDetails
                                                                                                                    {
                                                                                                                        TableNom = tn.TabNom,
                                                                                                                        Indexes = destinationDAL.TableIndex.Where(ti => ti.IdNcTable == tn.TableNcId)
                                                                                                                                                            .Select(i => i.TabNomIndex).ToArray()
                                                                                                                    }).ToList()
                                                                 };

                return listToReturn;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public interface IDestinationDataAcessLayer
    {
        Boolean InsertToDBB(List<SourceFormarteDonnees> source);
        IQueryable<SourceFormarteDonnees> GetAllItemsFromDBBFiltered(DateTime premierDateCom, DateTime derniereDateCom, DateTime premierFactureDateCom, DateTime dernierFactureDateCom, String nom, String prenom, String secteur);
    }
}

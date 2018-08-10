using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Catalogue = new HashSet<Catalogue>();
            CatalogueAutre = new HashSet<CatalogueAutre>();
            Commande = new HashSet<Commande>();
            Secteur = new HashSet<Secteur>();
            TableNomComptage = new HashSet<TableNomComptage>();
            Version = new HashSet<Version>();
            Visite = new HashSet<Visite>();
        }

        public int UtilId { get; set; }
        public int SourceId { get; set; }
        public string UtilNom { get; set; }
        public string UtilPrenom { get; set; }

        public ICollection<Catalogue> Catalogue { get; set; }
        public ICollection<CatalogueAutre> CatalogueAutre { get; set; }
        public ICollection<Commande> Commande { get; set; }
        public ICollection<Secteur> Secteur { get; set; }
        public ICollection<TableNomComptage> TableNomComptage { get; set; }
        public ICollection<Version> Version { get; set; }
        public ICollection<Visite> Visite { get; set; }
    }
}

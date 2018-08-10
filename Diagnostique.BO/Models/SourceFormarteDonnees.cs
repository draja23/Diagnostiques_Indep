using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diagnostique.BO.Models
{
    /// <summary>
    /// Par Rajaram.D Le 17/07/2018
    /// Représente tous les tables de la BDD [DiagnoInfoTerrain]. 
    /// Cette classe est utilisée pour transformer les données pour afficher sur la vue finale.
    /// </summary>
    public class SourceFormarteDonnees
    {
        public int ColId { get; set; }
        public int SourceFormarteUtilId { get; set; }
        public string SourceFormarteUtilNom { get; set; }
        public string SourceFormarteUtilPrenom { get; set; }
        public string SourceFormarteSectNom { get; set; }
        public string SourceFormarteCatalPrincipal { get; set; }
        public string SourceFormarteCatalAutrePrincipal { get; set; }
        public string SourceFormarteVersNom { get; set; }
        public int? SourceFormarteComPremierDateId { get; set; }
        public DateTime? SourceFormarteComPremierDate { get; set; }
        public int? SourceFormarteComDernierDateId { get; set; }
        public DateTime? SourceFormarteComDerniereDate { get; set; }
        public int? SourceFormarteComPremierFactureDateId { get; set; }
        public DateTime? SourceFormarteComPremierFactureDate { get; set; }
        public int? SourceFormarteComDernierFactureDateId { get; set; }
        public DateTime? SourceFormarteComDerniereFactureDate { get; set; }
        public DateTime? SourceFormarteVisDate { get; set; }
        public string[] SourceFormarteTabNom { get; set; }
        public string[] SourceFormarteTabLigneComptage { get; set; }
        public List<IndexDetails> SourceFormarteTabNomIndex { get; set; }
    }

    /// <summary>
    /// Par Rajaram.D Le 17/07/2018
    /// Extraction d'index de la table.
    /// </summary>
    public class IndexDetails
    {
        public string TableNom { get; set; }
        public string[] Indexes { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelSource
{
    /// <summary>
    /// Par Rajaram.D Le 17/07/2018
    /// Représente la table [DiagnoInfoTerrainSource].[dbo].[Donneesource]. 
    /// Cette classe est utilisée pour transformer les données de sources.
    /// </summary>
    public partial class DonneeSource
    {
        public int ColId { get; set; }
        public string SourceUtilNom { get; set; }
        public string SourceUtilPrenom { get; set; }
        public string SourceSectNom { get; set; }
        public string SourceCatalPrincipal { get; set; }
        public string SourceCatalAutrePrincipal { get; set; }
        public string SourceVersNom { get; set; }
        public int? SourceComPremierDateId { get; set; }
        public DateTime? SourceComPremierDate { get; set; }
        public int? SourceComDernierDateId { get; set; }
        public DateTime? SourceComDerniereDate { get; set; }
        public int? SourceComPremierFactureDateId { get; set; }
        public DateTime? SourceComPremierFactureDate { get; set; }
        public int? SourceComDernierFactureDateId { get; set; }
        public DateTime? SourceComDerniereFactureDate { get; set; }
        public DateTime? SourceVisDate { get; set; }
        public string SourceTabNom { get; set; }
        public string SourceTabLigneComptage { get; set; }
        public string SourceTabNomIndex { get; set; }
        public bool? EstCopieDestination { get; set; }
    }
}

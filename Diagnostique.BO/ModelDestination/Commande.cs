using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class Commande
    {
        public int ComId { get; set; }
        public int IdUtil { get; set; }
        public int? ComPremierDateId { get; set; }
        public DateTime? ComPremierDate { get; set; }
        public int? ComDernierDateId { get; set; }
        public DateTime? ComDerniereDate { get; set; }
        public int? ComPremierFactureDateId { get; set; }
        public DateTime? ComPremierFactureDate { get; set; }
        public int? ComDernierFactureDateId { get; set; }
        public DateTime? ComDerniereFactureDate { get; set; }

        public Utilisateur IdUtilNavigation { get; set; }
    }
}

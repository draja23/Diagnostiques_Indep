using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class TableNomComptage
    {
        public TableNomComptage()
        {
            TableIndex = new HashSet<TableIndex>();
        }

        public int TableNcId { get; set; }
        public int IdUtil { get; set; }
        public string TabNom { get; set; }
        public int? TabLigneComptage { get; set; }

        public Utilisateur IdUtilNavigation { get; set; }
        public ICollection<TableIndex> TableIndex { get; set; }
    }
}

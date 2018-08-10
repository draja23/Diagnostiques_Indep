using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class TableIndex
    {
        public int TableIndexId { get; set; }
        public int IdNcTable { get; set; }
        public string TabNomIndex { get; set; }

        public TableNomComptage IdNcTableNavigation { get; set; }
    }
}

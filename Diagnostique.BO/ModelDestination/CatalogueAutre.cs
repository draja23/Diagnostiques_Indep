using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class CatalogueAutre
    {
        public int CatalAutreId { get; set; }
        public int IdUtil { get; set; }
        public string CatalAutrePrincipal { get; set; }

        public Utilisateur IdUtilNavigation { get; set; }
    }
}

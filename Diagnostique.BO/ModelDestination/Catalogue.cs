using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class Catalogue
    {
        public int CatalId { get; set; }
        public int IdUtil { get; set; }
        public string CatalPrincipal { get; set; }

        public Utilisateur IdUtilNavigation { get; set; }
    }
}

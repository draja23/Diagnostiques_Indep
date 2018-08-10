using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class Version
    {
        public int VersId { get; set; }
        public int IdUtil { get; set; }
        public string VersNom { get; set; }

        public Utilisateur IdUtilNavigation { get; set; }
    }
}

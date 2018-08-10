using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class Secteur
    {
        public int SectId { get; set; }
        public int IdUtil { get; set; }
        public string SectNom { get; set; }

        public Utilisateur IdUtilNavigation { get; set; }
    }
}

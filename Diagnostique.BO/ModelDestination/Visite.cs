using System;
using System.Collections.Generic;

namespace Diagnostique.BO.ModelDestination
{
    public partial class Visite
    {
        public int VisId { get; set; }
        public int IdUtil { get; set; }
        public DateTime? VisDate { get; set; }

        public Utilisateur IdUtilNavigation { get; set; }
    }
}

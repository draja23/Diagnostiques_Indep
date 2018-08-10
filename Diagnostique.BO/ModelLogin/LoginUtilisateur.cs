using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diagnostique.BO.ModelLogin
{
    public partial class LoginUtilisateur
    {
        public int IdUtilisateur { get; set; }
        public string NomUtil { get; set; }
        public string PrenomUtil { get; set; }
        public string MailUtil { get; set; }
        public string MdpUtil { get; set; }
    }
}

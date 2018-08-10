using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diagnostique.BO.ModelLogin
{
    public class LoginDataAccessLayer : ILoginDataAccessLayer
    {
        //Contexte pour récupérer l'information d'un utilisateur de sélection.
        DiagnoInfoLoginTerrainContext _loginDAL = new DiagnoInfoLoginTerrainContext();

        /// <summary>
        /// Méthode pour séléctionner les informations d'un utilissateur,
        /// </summary>
        /// <param name="mail_util">Mail d'utilisateur</param>   
        /// <param name="mdpUtil">Mot de passe d'utilisateur</param>
        /// <returns>Retourne les infos LoginUtilisateur</returns>
        public LoginUtilisateur GetUserLoginInfos(string mailUtil, string mdpUtil)
        {
            try
            {
                //Récupération des données d'utilisateur.
                return _loginDAL.LoginUtilisateur.FirstOrDefault(u => u.MailUtil == mailUtil && u.MdpUtil == mdpUtil);
            }
            catch
            {
                return null;
            }

        }
    }
    public interface ILoginDataAccessLayer
    {
        LoginUtilisateur GetUserLoginInfos(string mailUtil, string mdpUtil);
    }
}

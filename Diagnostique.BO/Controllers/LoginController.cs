using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diagnostique.BO.ModelLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diagnostique.BO.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        //DAL OBJECT
        //LoginDataAccessLayer lDal = new LoginDataAccessLayer();
        //Logger
        readonly ILogger<LoginController> _logLC;
        //Accés aux données
        private readonly ILoginDataAccessLayer _loginDataAccessLayer;

        public LoginController(ILogger<LoginController> logLC, ILoginDataAccessLayer loginDataAccessLayer)
        {
            _loginDataAccessLayer = loginDataAccessLayer;
            _logLC = logLC;
        }

        /// <summary>
        /// Action pour récupérer les données d'un utilisateur...
        /// </summary>
        /// <param name="mail">Mail d'utilisateur</param>   
        /// <param name="mdp">mdp d'utilisateur</param> 
        /// <returns>Retourne l'information d'un utilisateur LoginUtilisateur</returns>
        // GET api/<controller>
        [HttpGet("[action]")]
        public LoginUtilisateur GetUserInfos(string mail, string mdp)
        {
            try
            {
                //Récupération des données en filtrant.
                // LoginUtilisateur uInfos = loginDataAccessLayer.GetUserLoginInfos(mail, mdp);
                return _loginDataAccessLayer.GetUserLoginInfos(mail, mdp);
            }
            catch (Exception ex)
            {
                _logLC.LogInformation("LoginController : InnerException = {0} - Message {1}", ex.InnerException, ex.Message);
                return null;
            }
        }
    }
}

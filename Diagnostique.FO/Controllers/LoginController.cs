using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Diagnostique.FO.Controllers
{
    [Route("api/[controller]")]
    public class LoginFoController : Controller
    {
        //DAL OBJECT
        //LoginDataAccessLayer lDal = new LoginDataAccessLayer();
        //Logger
        readonly ILogger<LoginFoController> _logLC;
        

        public LoginFoController(ILogger<LoginFoController> logLC)
        {          
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
        public async Task<dynamic> GetUserInfosAsync(string mail, string mdp)
        {
            try
            {
                //Récupération des données en filtrant.
                // LoginUtilisateur uInfos = loginDataAccessLayer.GetUserLoginInfos(mail, mdp);
                //LoginUtilisateur _logInfos = new LoginUtilisateur();
                //ApiHelper.ApiHelper _apiHelper = new ApiHelper.ApiHelper();
                //HttpClient client = _apiHelper.Initial();
                //HttpResponseMessage res = await client.GetAsync($"api/Login/GetUserInfos?mail={mail}&mdp={mdp}");
                //if(res.IsSuccessStatusCode)
                //{
                //    var returnResult = res.Content.ReadAsStringAsync().Result;
                //    _logInfos = JsonConvert.DeserializeObject<LoginUtilisateur>(returnResult);
                //}

                //Récupération des données en filtrant.
                dynamic _logInfos;
                ApiHelper.ApiHelper apiHelper = new ApiHelper.ApiHelper();
                _logInfos = await apiHelper.GetApiAsync<dynamic>($"api/Login/GetUserInfos?mail={mail}&mdp={mdp}");

                return _logInfos;

            }
            catch (Exception ex)
            {
                _logLC.LogInformation("LoginController FO: InnerException = {0} - Message {1}", ex.InnerException, ex.Message);
                return null;
            }
        }
    }
}
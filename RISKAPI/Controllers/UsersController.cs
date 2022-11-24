using DBStorage;
using DBStorage.ClassMetier;
using DBStorage.Mysql;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Ubiety.Dns.Core;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RISKAPI.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController
    {
        private static DAOFactory factory;

        public UsersController()
        {
            factory =  MySqlDAOFactory.GetInstance();
        }
        /// <summary>
        /// envoi un profil pour que l'API l'ajoute � la BDD
        /// </summary>
        /// <param name="pseudo">profil a rajouter a la BDD</param>
        /// <autor>Romain BARABANT</autor>
        [HttpPost("inscription")]
        public IActionResult inscription(Profil profil)
        {
            IActionResult reponse = null;
            try
            {
                ProfilDAO profilDAO = factory.CreerProfil();
                reponse = new AcceptedResult();
                if (profilDAO.VerifUserCreation(profil) == false)
                {
                    if (profil.Password.Length > 4)
                    {
                        profilDAO.Insert(profil);
                    }
                    else
                    {
                        reponse = new BadRequestObjectResult("password too short");
                        
                    }
                }
                else
                {
                    reponse = new BadRequestObjectResult("pseudo already used");
                }
            }
            catch (Exception e)
            {
                reponse = new BadRequestObjectResult(e.Message);
            }
            return reponse;
        }

        /// <summary>
        /// recupere un user dans la base de donnee
        /// </summary>
        /// <param name="profil">user a recuperer</param>
        /// <autor>Romain BARABANT</autor>
        [HttpPost("connexion")]
        public IActionResult connexion( Profil profil)
        {
            Profil profilDemande = null;
            ProfilDAO profilDAO = factory.CreerProfil();

            profil.Id = profilDAO.FindIdByPseudoProfil(profil.Pseudo);
      
            if (profil.Id != 0)
            {
                profilDemande = profil;
            }

            //Cr�ation de la r�ponse
            IActionResult actionResult = new NoContentResult();
            if (profilDemande != null)
            {
                string s = JsonSerializer.Serialize<Profil>(profilDemande);
                actionResult = new JsonResult(profilDemande);
            }
            
            return actionResult;
        }

        /// <summary>
        /// Request to verify user if exist in database
        /// </summary>
        /// <param name="profil">profil to find in database</param>
        /// <returns>boolean</returns>
        /// <Author>Charif Mahmoud,Brian VERCHERE</Author>
        [HttpGet("verifUser")]
        public IActionResult verifUser(Profil profil)
        {
            IActionResult reponse = null;
            ProfilDAO profilDAO = factory.CreerProfil();

            bool res = profilDAO.VerifUserCreation(profil);
     
            if (res == null)
            {
                reponse = new BadRequestResult();
            }
            else
            {
                reponse = new JsonResult(res);
            }

            return reponse;
        }
    }
}
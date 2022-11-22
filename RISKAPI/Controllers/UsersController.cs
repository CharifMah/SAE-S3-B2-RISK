using DBStorage;
using DBStorage.ClassMetier;
using DBStorage.Mysql;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        /// envoi un profil pour que l'API l'ajoute à la BDD
        /// </summary>
        /// <param name="pseudo">profil a rajouter a la BDD</param>
        /// <autor>Romain BARABANT</autor>
        [HttpPost("inscription")]
        public IActionResult inscription(string pseudo)
        {
            IActionResult reponse = null;
            try
            {
                Profil profil = new Profil(pseudo);
                ProfilDAO profilDAO = factory.CreerProfil();
                reponse = new AcceptedResult();
                if (profilDAO.VerifUserCreation(profil) == false)
                {
                    profilDAO.Insert(profil);
                }
                else
                {
                    reponse = new BadRequestResult();
                }
            }
            catch (Exception e)
            {
                reponse = new BadRequestResult();
            }
            return reponse;
        }

        /// <summary>
        /// recupere un user dans la base de donnee
        /// </summary>
        /// <param name="pseudo">Pseudo du user a recuperer</param>
        /// <autor>Romain BARABANT</autor>
        [HttpGet("connexion")]
        public IActionResult connexion(string pseudo)
        {
            Profil profilDemandee = null;
            Profil p = new Profil("");
            ProfilDAO profilDAO = factory.CreerProfil();

            p.Id = profilDAO.FindIdByPseudoProfil(pseudo);
      
            if (p.Id != 0)
            {
                p.Pseudo = pseudo;
                profilDemandee = p;
            }

            //Création de la réponse
            IActionResult actionResult = new NoContentResult();
            if (profilDemandee != null)
            {
                actionResult = new JsonResult(profilDemandee);
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
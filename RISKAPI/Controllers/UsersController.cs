using DBStorage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RISKAPI.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController
    {
        /// <summary>
        /// envoi un profil pour que l'API l'ajoute à la BDD
        /// </summary>
        /// <param name="pseudo">profil a rajouter a la BDD</param>
        /// <autor>Romain BARABANT</autor>
        [HttpPost("inscription")]
        public IActionResult inscription(string pseudo)
        {
            GestionDatabase connection = new GestionDatabase();

            IActionResult reponse = null;
            try
            {
                Profil profil = new Profil(pseudo);
                reponse = new AcceptedResult();
                connection.CreateUser(profil.Pseudo);
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
            GestionDatabase connection = new GestionDatabase();
            Profil profilDemandee = null;
            Profil p = new Profil();

            p.Pseudo = connection.SelectUser(pseudo);
            if (p.Pseudo != null)
            {
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

        [HttpGet("verifUser")]
        public IActionResult verifUser(string pseudo)
        {
            IActionResult reponse = null;

            GestionDatabase connection = new GestionDatabase();
            bool res = connection.VerifUserCreation(pseudo);
     
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
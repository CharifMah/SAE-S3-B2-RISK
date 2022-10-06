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
        /// <param name="jsonLogin">profil a rajouter a la BDD</param>
        /// <autor>Romain BARABANT</autor>
        [HttpPost("inscription")]
        public IActionResult inscription([FromBody]string jsonLogin)
        {
            GestionDatabase connection = new GestionDatabase();

            IActionResult reponse = null;
            try
            {
                Profil profil = JsonConvert.DeserializeObject<Profil>(jsonLogin);
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
        /// <param name="login">Pseudo du user a recuperer</param>
        /// <autor>Romain BARABANT</autor>
        [HttpGet("connexion")]
        public IActionResult connexion(string login)
        {
            GestionDatabase connection = new GestionDatabase();
            Profil profilDemandee = null;
            Profil p = new Profil();
            p.Pseudo = connection.SelectUser(login);
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

    }
}
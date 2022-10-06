using DBStorage;
using Microsoft.AspNetCore.Mvc;

namespace RISKAPI.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController
    {

        [HttpGet("Login")]
        public IActionResult Donne(int id)
        {
            GestionDatabase connection = new GestionDatabase();

            connection.CreateDatabase();
            connection.CreateUserTable();
            connection.CreateUser("romain");
            connection.SelectUser("romain");

            Profil personneDemandee = null;
            switch (id)
            {
                case 0: personneDemandee = new Profil("Alice"); break;
                case 1: personneDemandee = new Profil("Bob"); break;
            }

            //Création de la réponse
            IActionResult actionResult = new NoContentResult();     //Réponse par défaut dans notre cas (pas de content car la personne n'existe pas)
            if (personneDemandee != null)
            {
                actionResult = new JsonResult(personneDemandee);    //Si la personne existe, l'API la renvoie en JSon
            }
            return actionResult;

        }
    }
}
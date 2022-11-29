using DBStorage.ClassMetier;
using DBStorage.DAO;
using DBStorage.DAOFactory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RISKAPI.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {

        /// <summary>
        /// envoi un profil pour que l'API l'ajoute a la BDD
        /// </summary>
        /// <param name="profil">profil a rajouter a la BDD</param>
        /// <autor>Romain BARABANT</autor>
        [HttpPost("inscription")]
        public IActionResult inscription(Profil profil)
        {
            IActionResult reponse = null;
            try
            {
                ProfilDAO profilDAO = MySqlDAOFactory.Get().CreerProfil();
                reponse = new AcceptedResult();
                if (profilDAO.VerifUserCreation(profil) == false)
                {
                    if (profil.Password.Length > 4)
                    {
                        PasswordHasher<Profil> passwordHasher = new PasswordHasher<Profil>();
                        profil.Password = passwordHasher.HashPassword(profil, profil.Password);
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
        public IActionResult connexion(Profil profil)
        {
            IActionResult reponse = null;
            try
            {
                Profil profilDemande = null;
                ProfilDAO profilDAO = MySqlDAOFactory.Get().CreerProfil();
                int Id = profilDAO.FindIdByPseudoProfil(profil.Pseudo);
                if (Id != 0)
                {
                    string[] properties = profilDAO.FindByIdProfil(Id).Split(',');
                    profilDemande = new Profil(properties[1], properties[2]);
                    PasswordHasher<Profil> passwordHasher = new PasswordHasher<Profil>();
                    if (passwordHasher.VerifyHashedPassword(profilDemande, profilDemande.Password, profil.Password) != 0)
                    {
                        reponse = new JsonResult(profilDemande);
                    }
                    else
                    {
                        reponse = new BadRequestObjectResult("wrong password");
                    }
                }
                else
                {
                    reponse = new BadRequestObjectResult("this account do not exist try to register or use an ather pseudo");
                }
            }
            catch (Exception e)
            {
                reponse = new BadRequestObjectResult(e.Message);
            }

            return reponse;
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
            ProfilDAO profilDAO = MySqlDAOFactory.Get().CreerProfil();

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
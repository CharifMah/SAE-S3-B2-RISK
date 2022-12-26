using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using ModelsAPI.ClassMetier;
using Redis.OM.Searching;
using Redis.OM;
using System;

namespace RISKAPI.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly RedisCollection<Profil> _people;
        private readonly RedisConnectionProvider _provider;
        public UsersController(RedisConnectionProvider provider)
        {
            _provider = provider;
            _people = (RedisCollection<Profil>)provider.RedisCollection<Profil>();
        }

        /// <summary>
        /// envoi un profil pour que l'API l'ajoute a la BDD
        /// </summary>
        /// <param name="profil">profil a rajouter a la BDD</param>
        /// <autor>Romain BARABANT</autor>
        [HttpPost("inscription")]
        public async Task<IActionResult> inscription(Profil profil)
        {
            IActionResult reponse = null;
            try
            {       
                reponse = new AcceptedResult();
                if (_provider.Connection.Get($"Profil:{profil.Id}"))
                {
                    if (profil.Password.Length > 4)
                    {
                        PasswordHasher<Profil> passwordHasher = new PasswordHasher<Profil>();
                        profil.Password = passwordHasher.HashPassword(profil, profil.Password);
                        await _people.InsertAsync(profil);

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
                //ProfilDAO profilDAO = MySqlDAOFactory.Get().CreerProfil();
                //int Id = profilDAO.FindIdByPseudoProfil(profil.Pseudo);
                //if (Id != 0)
                //{
                //    string[] properties = profilDAO.FindByIdProfil(Id).Split(',');
                //    profilDemande = new Profil(properties[1], properties[2]);
                //    PasswordHasher<Profil> passwordHasher = new PasswordHasher<Profil>();
                //    if (passwordHasher.VerifyHashedPassword(profilDemande, profilDemande.Password, profil.Password) != 0)
                //    {
                //        reponse = new JsonResult(profilDemande);
                //    }
                //    else
                //    {
                //        reponse = new BadRequestObjectResult("wrong password");
                //    }
                //}
                //else
                //{
                //    reponse = new BadRequestObjectResult("this account do not exist try to register or use an ather pseudo");
                //}
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
            //ProfilDAO profilDAO = MySqlDAOFactory.Get().CreerProfil();

            //bool res = profilDAO.VerifUserCreation(profil);

            //if (res == null)
            //{
            //    reponse = new BadRequestResult();
            //}
            //else
            //{
            //    reponse = new JsonResult(res);
            //}

            return reponse;
        }
    }
}
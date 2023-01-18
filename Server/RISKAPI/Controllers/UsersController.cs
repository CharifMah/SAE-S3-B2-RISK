using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using NReJSON;
using Redis.OM;
using Redis.OM.Searching;
using RISKAPI.Services;
using StackExchange.Redis;

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
                string key = $"Profil:{profil.Pseudo}";
                reponse = new AcceptedResult();
                if (!RedisProvider.Instance.RedisDataBase.KeyExists(key))
                {
                    if (profil.Password.Length > 4)
                    {
                        PasswordHasher<Profil> passwordHasher = new PasswordHasher<Profil>();
                        profil.Password = passwordHasher.HashPassword(profil, profil.Password);
                        await _people.InsertAsync(profil);
                        reponse = new JsonResult("Profil Created");

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
        public async Task<IActionResult> connexion(Profil profil)
        {
            IActionResult reponse = null;
            try
            {
                string key = $"Profil:{profil.Pseudo}";
                string[] parms = { "." };

                RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key, parms);
                if (!result.IsNull)
                {
                    Profil profilDemander = JsonConvert.DeserializeObject<Profil>(result.ToString());
                    PasswordHasher<Profil> passwordHasher = new PasswordHasher<Profil>();
                    if (passwordHasher.VerifyHashedPassword(profilDemander, profilDemander.Password, profil.Password) != 0)
                    {
                        reponse = new JsonResult(profilDemander);
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
        /// <returns>boolean true if user exist</returns>
        /// <Author>Charif Mahmoud</Author>
        [HttpGet("verifUser")]
        public IActionResult verifUser(string pseudo)
        {
            IActionResult reponse = null;

            bool res = RedisProvider.Instance.RedisDataBase.KeyExists($"Profil:{pseudo}");

            reponse = new JsonResult(res);

            return reponse;
        }
    }
}
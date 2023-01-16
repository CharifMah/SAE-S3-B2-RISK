using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.GameStatus;
using Redis.OM;
using Redis.OM.Searching;
using RISKAPI.Services;

namespace RISKAPI.Controllers
{
    [Route("Lobby")]
    [ApiController]
    public class LobbyController : Controller
    {

        private readonly RedisCollection<Lobby> _lobby;
        private readonly RedisConnectionProvider _provider;
        public LobbyController(RedisConnectionProvider provider)
        {
            _provider = provider;
            _lobby = (RedisCollection<Lobby>)provider.RedisCollection<Lobby>();
        }

        [HttpPost("CreateLobby")]
        public async Task<IActionResult> CreateLobby(Lobby lobby)
        {
            IActionResult reponse = null;
            try 
            {
                bool res = RedisProvider.Instance.RedisDataBase.KeyExists($"Lobbys:{lobby.Id}");
                if (!res)
                {
                    PasswordHasher<Lobby> passwordHasher = new PasswordHasher<Lobby>();
                    lobby.Password = passwordHasher.HashPassword(lobby, lobby.Password);
                    await _lobby.InsertAsync(lobby);
                    List<Lobby> lobbyList = JurasicRiskGameServer.Get.Lobbys;
                    if (lobbyList.Find(l => l.Id == lobby.Id) == null)
                    {
                        lobbyList.Add(lobby);
                    }
                    Console.WriteLine("Created Lobbys");

                    reponse = new JsonResult($"Lobbys With Name : {lobby.Id} created");
                }
                else
                {
                    reponse = new JsonResult("This key Already Exist");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                reponse = new BadRequestObjectResult(e.Message);
            }
            return reponse;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ModelsAPI.ClassMetier;
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
                bool res = RedisProvider.Instance.RedisDataBase.KeyExists($"Lobby:{lobby.Id}");
                if (!res)
                {
                    await _lobby.InsertAsync(lobby);
                    Console.WriteLine("Created Lobby");

                    reponse = new JsonResult($"Lobby With Name : {lobby.Id} created");
                }
                else
                {
                    reponse = new JsonResult($"A lobby with name {lobby.Id} already exist");
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

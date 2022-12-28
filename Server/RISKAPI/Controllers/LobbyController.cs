using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Graph;
using ModelsAPI.ClassMetier;
using ModelsAPI.ClassMetier.Player;
using Newtonsoft.Json;
using NReJSON;
using Redis.OM;
using Redis.OM.Searching;
using RISKAPI.Services;
using StackExchange.Redis;

namespace RISKAPI.Controllers
{
    [Route("Lobby")]
    [ApiController]
    public class LobbyController : Controller
    {

        private readonly IDistributedCache _cache;
        private readonly RedisCollection<Lobby> _lobby;
        private readonly RedisConnectionProvider _provider;
        public LobbyController(RedisConnectionProvider provider, IDistributedCache cache)
        {
            _provider = provider;
            _lobby = (RedisCollection<Lobby>)provider.RedisCollection<Lobby>();
            _cache = cache;

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
                reponse = new BadRequestObjectResult(e.Message);
            }
            return reponse;
        }

        [HttpPut("UpdateLobby")]
        public async Task<IActionResult> UpdateLobby(Lobby lobby)
        {
            IActionResult reponse = null;
            try
            {
                string key = $"Lobby:{lobby.Id}";
                reponse = new AcceptedResult();
                if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
                {
                    await _lobby.UpdateAsync(lobby);

                    reponse = new JsonResult("Updated");
                }
                else
                {
                    reponse = new JsonResult("Lobby not found");
                }

            }
            catch (Exception e)
            {
                reponse = new BadRequestObjectResult(e.Message);
            }
            return reponse;
        }

        // GET api/<LobbyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLobby(string id)
        {
            IActionResult reponse = null;
            try
            {
                string key = $"Lobby:{id}";


                if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
                {
                    RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                    Lobby? _lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());
                    if (_lobby != null)
                        reponse = new JsonResult(_lobby);
                    else
                        reponse = new BadRequestObjectResult("Error Durring Deserialisation");

                }
                else
                {
                    reponse = new BadRequestObjectResult("Lobby don't exist");
                }

            }
            catch (Exception e)
            {
                reponse = new BadRequestObjectResult(e.Message);
            }

            return reponse;
        }

        [HttpPut("PutTeam/{lobbyName}/{playerName}")]
        public async Task<IActionResult> SetTeam(string lobbyName, string playerName, [FromBody] Teams Team)
        {
            IActionResult reponse = null;
            try
            {
                string key = $"Lobby:{lobbyName}";
                reponse = new AcceptedResult();
                if (RedisProvider.Instance.RedisDataBase.KeyExists(key))
                {
                    RedisResult result = await RedisProvider.Instance.RedisDataBase.JsonGetAsync(key);
                    Lobby? lobby = JsonConvert.DeserializeObject<Lobby>(result.ToString());

                    lobby.Joueurs.FindLast(x => x.Profil.Pseudo == playerName).Team = Team;

                    await _lobby.UpdateAsync(lobby);

                    reponse = new JsonResult("Updated");
                }
                else
                {
                    reponse = new JsonResult("Lobby not found");
                }

            }
            catch (Exception e)
            {
                reponse = new BadRequestObjectResult(e.Message);
            }
            return reponse;
        }
    }
}

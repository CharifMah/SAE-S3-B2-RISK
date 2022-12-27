using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ModelsAPI.ClassMetier;
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
        public async Task<IActionResult> CreateLobby(string id)
        {
            IActionResult reponse = null;
            try
            {
                Lobby lobby = new Lobby();
                lobby.Id = id;
                await _lobby.InsertAsync(lobby);
                reponse = new JsonResult($"Lobby With Name : {id} created");
            }
            catch (Exception e)
            {
                reponse = new BadRequestObjectResult(e.Message);
            }
            return reponse;
        }

        [HttpPost("SetLobby")]
        public async Task<IActionResult> SetLobby(Lobby lobby)
        {
            IActionResult reponse = null;
            try
            {
                string key = $"Lobby:{lobby.Id}";
                reponse = new AcceptedResult();
                if (RedisConnectorHelper.Connection.GetDatabase(0).KeyExists(key))
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


                if (RedisConnectorHelper.Connection.GetDatabase(0).KeyExists(key))
                {
                    RedisResult result = await RedisConnectorHelper.Connection.GetDatabase(0).JsonGetAsync(key);
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
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ModelsAPI.ClassMetier.Map;
using Redis.OM.Searching;
using Redis.OM;
using RISKAPI.Services;
using NReJSON;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RISKAPI.Controllers
{
    [Route("Carte")]
    [ApiController]
    public class CarteController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly RedisCollection<Carte> _carte;
        private readonly RedisConnectionProvider _provider;
        public CarteController(RedisConnectionProvider provider, IDistributedCache cache)
        {
            _provider = provider;
            _carte = (RedisCollection<Carte>)provider.RedisCollection<Carte>();
            _cache = cache;
        }

        // POST api/<CarteController>

        [HttpPost("SetCarte")]
        public IActionResult Post(Carte carte)
        {
            IActionResult reponse = null;
            try
            {
                RedisProvider.Instance.RedisDataBase.JsonSetAsync("Carte", JsonConvert.SerializeObject(carte));
                reponse = new AcceptedResult();
            }
            catch (Exception e)
            {
                reponse = new BadRequestObjectResult(e.Message);
            }
            return reponse;
        }
    }
}

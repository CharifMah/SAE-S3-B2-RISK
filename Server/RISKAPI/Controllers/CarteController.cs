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

        // GET api/<CarteController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult reponse = null;
            return reponse;
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

        // PUT api/<CarteController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            IActionResult reponse = null;
            return reponse;
        }

        // DELETE api/<CarteController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult reponse = null;
            return reponse;
        }
    }
}

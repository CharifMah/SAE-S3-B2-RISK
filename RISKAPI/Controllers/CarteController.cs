using Microsoft.AspNetCore.Mvc;
using ModelsAPI.ClassMetier.Map;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RISKAPI.Controllers
{
    [Route("Carte")]
    [ApiController]
    public class CarteController : ControllerBase
    {
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

                //MySqlDAOFactory.Get().CreateCarteDAO().Insert(carte as Carte);
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

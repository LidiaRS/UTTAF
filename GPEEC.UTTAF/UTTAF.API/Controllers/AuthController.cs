using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AuthTaskAsync([FromBody]AuthModel auth)
        {
            return Created("", auth);
        }

        [HttpGet]
        public async Task<IActionResult> AuthTaskAsync(int id)
        {
            if (id == 10)
                return Ok($"Seu id é {id}");

            return BadRequest("é necessario informar o id = 10");
        }
    }
}
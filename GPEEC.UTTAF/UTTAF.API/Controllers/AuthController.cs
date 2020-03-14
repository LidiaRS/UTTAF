using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Models;

namespace UTTAF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public AuthController(IAuthRepository repository) => _repository = repository;

        [HttpPost]
        public async Task<IActionResult> AuthTaskAsync([FromBody]AuthModel auth)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAuthTaskAsync(auth);
                return Created("", auth);
            }

            return BadRequest();
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
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
            if (await _repository.ExistsAuthTaskAsync(auth))
                return Conflict();

            await _repository.AddAuthAsync(auth);
            return Created("", auth);
        }
    }
}
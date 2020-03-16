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
        public async Task<IActionResult> AuthSessionTaskAsync([FromBody]AuthModel auth)
        {
            if (await _repository.ExistsTaskAsync(auth))
                return Conflict("Ja existe uma sessao com esse referencial em andamento.");

            await _repository.AddAsync(auth);
            return Created("", auth);
        }

        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveSessionTaskAsync([FromBody]AuthModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _repository.ExistsTaskAsync(model))
                {
                    await _repository.RemovehAsync(model);

                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
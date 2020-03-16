using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Enums;

namespace UTTAF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public SessionController(IAuthRepository repository) => _repository = repository;

        [HttpPost]
        public async Task<IActionResult> AuthSessionTaskAsync([FromBody]AuthSessionModel auth)
        {
            if (await _repository.ExistsTaskAsync(auth))
                return Conflict("Ja existe uma sessao com esse referencial em andamento.");

            auth.SessionStatus = SessionStatusEnum.Active;
            auth.SessionDate = DateTime.Now;

            await _repository.AddAsync(auth);
            return Created("", auth);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSessionTaskAsync([FromBody]AuthSessionModel model)
        {
            if (await _repository.RemoveAsync(model))
                return Ok();

            return NotFound();
        }
    }
}
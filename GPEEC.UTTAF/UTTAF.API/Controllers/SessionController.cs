using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Models;
using UTTAF.Dependencies.Services;

namespace UTTAF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAttendeeRepository _attendeeRepository;

        public SessionController(ISessionRepository sessionRepository, IAttendeeRepository attendeeRepository)
        {
            _sessionRepository = sessionRepository;
            _attendeeRepository = attendeeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSessionTaskAsync([FromBody]AuthSessionModel auth)
        {
            if (await _sessionRepository.ExistsTaskAsync(auth))
                return Conflict("Ja existe uma sessao com esse referencial ativo/em andamento.");

            auth.SessionStatus = SessionStatusEnum.Active;
            auth.SessionDate = DateTime.Now;
            auth.SessionPassword = SecurityService.CalculateHash256(auth.SessionPassword);

            await _sessionRepository.AddAsync(auth);
            return Created(string.Empty, auth);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSessionTaskAsync([FromBody]AuthSessionModel model)
        {
            model.SessionPassword = SecurityService.CalculateHash256(model.SessionPassword);
            if (await _sessionRepository.RemoveTaskAsync(model))
                if (await _attendeeRepository.ClearAttendeersTaskAsync(model))
                    return Ok();

            return NotFound($"Nao foi possivel encontrar uma sessao com o nome: {model.SessionReference}");
        }
    }
}
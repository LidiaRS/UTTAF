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
        public async Task<IActionResult> CreateSessionTaskAsync([FromBody]AuthSessionModel model)
        {
            if (await _sessionRepository.ExistsTaskAsync(model))
                return Conflict("Ja existe uma sessao com esse referencial ativo/em andamento.");

            model.SessionStatus = SessionStatusEnum.Active;
            model.SessionDate = DateTime.Now;
            model.SessionPassword = SecurityService.CalculateHash256(model.SessionPassword);

            await _sessionRepository.AddAsync(model);
            return Created(string.Empty, model);
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

        [HttpGet("Started")]
        public async Task<IActionResult> SessionStartedTaskAsync(string reference)
        {
            if (!string.IsNullOrEmpty(reference))
            {
                if (!await _sessionRepository.ExistsTaskAsync(reference))
                    return NotFound("A Sessao informada nao existe");

                if (await _sessionRepository.SessionStartedTaskAsync(reference))
                    return Ok(reference);

                return NotFound("A sessao informada nao está em andamento");
            }

            return BadRequest("Informe o referencial da sessao.");
        }

        [HttpPut("Status")]
        public async Task<IActionResult> ChangeSessionStatus([FromBody]AuthSessionModel model)
        {
            return BadRequest();
        }
    }
}
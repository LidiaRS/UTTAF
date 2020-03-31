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
        public async Task<IActionResult> CreateSessionTaskAsync([FromBody]AuthSessionModel authSession)
        {
            if (await _sessionRepository.ExistsTaskAsync(authSession))
                return Conflict("Ja existe uma sessao com esse referencial ativo/em andamento.");

            authSession.SessionStatus = SessionStatusEnum.Active;
            authSession.SessionDate = DateTime.Now;
            authSession.SessionPassword = SecurityService.CalculateHash256(authSession.SessionPassword);

            if (await _sessionRepository.AddAsync(authSession) is AuthSessionModel auth)
                return Created(string.Empty, auth);

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSessionTaskAsync([FromBody]AuthSessionModel authSession)
        {
            authSession.SessionPassword = SecurityService.CalculateHash256(authSession.SessionPassword);
            if (await _sessionRepository.RemoveTaskAsync(authSession))
                if (await _attendeeRepository.ClearAttendeersTaskAsync(authSession))
                    return Ok();

            return NotFound($"Nao foi possivel encontrar uma sessao com o nome: {authSession.SessionReference}");
        }

        [HttpGet("Started")]
        public async Task<IActionResult> SessionStartedTaskAsync(string sessionReference)
        {
            if (!string.IsNullOrEmpty(sessionReference))
            {
                if (!await _sessionRepository.ExistsTaskAsync(sessionReference))
                    return NotFound("A Sessao informada nao existe");

                if (await _sessionRepository.SessionStartedTaskAsync(sessionReference))
                    return Ok(sessionReference);

                return BadRequest("A sessao informada nao está em andamento");
            }

            return BadRequest("Informe o referencial da sessao.");
        }

        [HttpPut("Status")]
        public async Task<IActionResult> ChangeSessionStatusTaskAsync([FromBody]AuthSessionModel authSession)
        {
            if (ModelState.IsValid)
            {
                if (await _sessionRepository.ExistsTaskAsync(authSession.SessionReference))
                    return NotFound("A sessao informada nao existe.");

                authSession.SessionPassword = SecurityService.CalculateHash256(authSession.SessionPassword);

                if (await _sessionRepository.ChangeStatusSessionTaskAsync(authSession) is AuthSessionModel auth)
                    return Ok(auth);

                return BadRequest("Nao foi possivel alterar o status, verifique se a senha está correta.");
            }
            return BadRequest();
        }
    }
}
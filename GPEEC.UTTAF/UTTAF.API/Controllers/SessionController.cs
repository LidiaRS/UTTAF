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
        private readonly ISessionRepository _repository;

        public SessionController(ISessionRepository repository) => _repository = repository;

        [HttpPost]
        public async Task<IActionResult> AuthSessionTaskAsync([FromBody]AuthSessionModel auth)
        {
            if (await _repository.ExistsTaskAsync(auth))
                return Conflict("Ja existe uma sessao com esse referencial ativo/em andamento.");

            auth.SessionStatus = SessionStatusEnum.Active;
            auth.SessionDate = DateTime.Now;
            auth.SessionPassword = SecurityService.CalculateHash256(auth.SessionPassword);

            await _repository.AddAsync(auth);
            return Created("", auth);
        }

        [HttpPost("attendees")]
        public async Task<IActionResult> JoinAtSessionTaskAsync([FromBody]AttendeeModel attendee)
        {
            if (await _repository.AddAttendeeTaskAsync(attendee))
                return Created("", attendee);

            return Conflict("Ja existe um participante com esse nome, ou o referencial informado nao existe.");
        }

        [HttpGet("attendees")]
        public async Task<IActionResult> AttendeesInSessionTaskAsync(string sessionReference, string sessionPassword)
        {
            if (await _repository.ExistsTaskAsync(sessionReference, SecurityService.CalculateHash256(sessionPassword)))
                return Ok(await _repository.GetAttendersTaskAsync(sessionReference));

            return NotFound("Sessao informada nao existe.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSessionTaskAsync([FromBody]AuthSessionModel model)
        {
            model.SessionPassword = SecurityService.CalculateHash256(model.SessionPassword);
            if (await _repository.RemoveTaskAsync(model))
                return Ok();

            return NotFound();
        }
    }
}
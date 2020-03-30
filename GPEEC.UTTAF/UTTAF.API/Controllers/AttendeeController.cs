using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Models;
using UTTAF.Dependencies.Services;

namespace UTTAF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly ISessionRepository _sessionRepository;

        public AttendeeController(IAttendeeRepository attendeeRepository, ISessionRepository sessionRepository)
        {
            _attendeeRepository = attendeeRepository;
            _sessionRepository = sessionRepository;
        }

        [HttpPost("Join")]
        public async Task<IActionResult> JoinAtSessionTaskAsync([FromBody]AttendeeModel attendee)
        {
            if (await _attendeeRepository.AddAttendeeTaskAsync(attendee))
                return Created(string.Empty, attendee);

            return Conflict("Ja existe um participante com esse nome, ou o referencial informado nao existe.");
        }

        [HttpGet]
        public async Task<IActionResult> AttendeesInSessionTaskAsync(string sessionReference, string sessionPassword)
        {
            if (!string.IsNullOrEmpty(sessionReference) && !string.IsNullOrEmpty(sessionPassword))
            {
                if (await _sessionRepository.ExistsTaskAsync(sessionReference, SecurityService.CalculateHash256(sessionPassword)))
                    return Ok(await _attendeeRepository.GetAttendersTaskAsync(sessionReference));

                return NotFound("Sessao informada nao existe.");
            }

            return BadRequest();
        }
    }
}
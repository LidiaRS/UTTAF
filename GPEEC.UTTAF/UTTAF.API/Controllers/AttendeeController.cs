using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;
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
            if (ModelState.IsValid)
            {
                if (!await _sessionRepository.ExistsTaskAsync(attendee.SessionReference))
                    return NotFound("O referencial informado nao existe.");

                if (await _sessionRepository.SessionStartedTaskAsync(attendee.SessionReference))
                    return BadRequest("A sessao ja está em andamento");

                if (await _attendeeRepository.AddAttendeeTaskAsync(attendee) is AttendeeModel att)
                    return Created(string.Empty, att);

                return Conflict("Ja existe um participante com esse nome.");
            }

            return BadRequest("É necessario informar o referencial da sessao e o nome do participante!");
        }

        [HttpDelete("Leave")]
        public async Task<IActionResult> LeaveAtSessionTaskAsync([FromBody]AttendeeModel attendee)
        {
            if (ModelState.IsValid)
            {
                if (!await _sessionRepository.ExistsTaskAsync(attendee.SessionReference))
                    return NotFound("A sessao informada nao existe, talvez tenha sido excluida!");

                if (await _attendeeRepository.LeaveAttendeeTaskAsync(attendee))
                    return Ok(attendee);

                return Conflict("Nao existe um participante com esse Id, ou o referencial informado nao existe.");
            }

            return BadRequest("É necessario informar as informaçoes do participante!");
        }

        [HttpGet]
        public async Task<IActionResult> AttendeesInSessionTaskAsync([Required]string sessionReference, [Required]string sessionPassword)
        {
            if (ModelState.IsValid)
            {
                if (await _sessionRepository.ExistsTaskAsync(sessionReference, SecurityService.CalculateHash256(sessionPassword)))
                    return Ok(await _attendeeRepository.GetAttendersTaskAsync(sessionReference));

                return NotFound("Sessao informada nao existe.");
            }

            return BadRequest();
        }
    }
}
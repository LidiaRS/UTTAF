using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using UTTAF.API.Models.Auxiliary;
using UTTAF.API.Repository.Interfaces;

namespace UTTAF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IRobotRepository _robotRepository;

        public RobotController(ISessionRepository sessionRepository, IRobotRepository robotRepository)
        {
            _sessionRepository = sessionRepository;
            _robotRepository = robotRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ExistsTaskAsync([Required]string sessionReference)
        {
            if (ModelState.IsValid)
            {
                if (await _robotRepository.ExistsTaskAsync(sessionReference))
                    return Ok(sessionReference);

                return NotFound("Nao existe nenhum robo na sessao informada");
            }

            return BadRequest();
        }

        [HttpGet("InMoving")]
        public async Task<IActionResult> InMovingTaskAsync([Required]string sessionReference)
        {
            if (ModelState.IsValid)
            {
                if (await _robotRepository.InMovingTaskAsync(sessionReference))
                    return Ok(sessionReference);

                return NotFound("O robo ainda nao está em movimento");
            }

            return BadRequest();
        }

        [HttpPut("Status")]
        public async Task<IActionResult> ChangeStatusRobotTaskAsync()
        {
            return BadRequest();
        }

        [HttpGet("FoundCard")]
        public async Task<IActionResult> FoundCardTaskAsync()
        {
            return BadRequest();
        }

        [HttpPost("FoundCard")]
        public async Task<IActionResult> FoundCardTaskAsync([FromBody]CardModel card)
        {
            if (ModelState.IsValid)
            {
                if (!await _sessionRepository.ExistsTaskAsync(card.SessionReference, card.SessionPassword))
                    return NotFound("A sessao informada nao existe, ou a senha está incorreta");

                //fazer o carrinho para de andar.
            }

            return BadRequest();
        }
    }
}
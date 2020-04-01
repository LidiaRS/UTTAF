using Microsoft.AspNetCore.Mvc;

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

        public RobotController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> IsMovingTaskAsync(string sessionReference)
        {
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeStatusRobotTaskAsync(string sessionReference)
        {
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> FoundCardTaskAsync([FromBody]CardModel card)
        {
            if (ModelState.IsValid)
            {
                if(!await _sessionRepository.ExistsTaskAsync(card.SessionReference, card.SessionPassword))
                    return NotFound("A sessao informada nao existe, ou a senha está incorreta");

                //vefiricar se o robo ja está andando.
            }

            return BadRequest();
        }
    }
}
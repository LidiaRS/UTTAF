using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Enums;

namespace UTTAF.API.Repository
{
    public class RobotRepository : Repository, IRobotRepository
    {
        public RobotRepository(DataContext context) : base(context)
        {
        }

        public async Task<RobotModel> AddTaskAsync(RobotModel robot)
        {
            if ((await _context.Robots.AddAsync(robot)).Entity is RobotModel bot)
            {
                await _context.SaveChangesAsync();
                return bot;
            }

            return default;
        }

        public async Task<bool> ExistsTaskAsync(string sessionReference) =>
            await _context.Robots.AnyAsync(x => x.SessionReference == sessionReference);

        public async Task<bool> InMovingTaskAsync(string sessionReference) =>
            await _context.Robots.AnyAsync(x => x.SessionReference == sessionReference && x.RobotStatus == RobotStatusEnum.InMoving);
    }
}
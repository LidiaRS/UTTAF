using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;

namespace UTTAF.API.Repository
{
	public class RobotRepository : Repository, IRobotRepository
	{
		public RobotRepository(DataContext context) : base(context)
		{
		}

		public async Task<RobotModel> AddTaskAsync(RobotModel robot)
		{
			RobotModel bot = (await _context.Robots.AddAsync(robot)).Entity;
			await _context.SaveChangesAsync();
			return robot;
		}

		public async Task<RobotModel> FindBySessionReferenceTaskAsync(string sessionReference) =>
			await _context.Robots.SingleOrDefaultAsync(x => x.SessionReference == sessionReference);
	}
}
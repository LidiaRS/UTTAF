using System.Threading.Tasks;

using UTTAF.API.Models;

namespace UTTAF.API.Repository.Interfaces
{
	public interface IRobotRepository
	{
		Task<RobotModel> AddTaskAsync(RobotModel robot);

		Task<RobotModel> FindBySessionReferenceTaskAsync(string sessionReference);
	}
}
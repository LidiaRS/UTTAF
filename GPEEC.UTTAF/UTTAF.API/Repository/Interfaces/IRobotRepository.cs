using System.Threading.Tasks;

using UTTAF.API.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IRobotRepository
    {
        Task<RobotModel> AddTaskAsync(RobotModel robot);

        Task<bool> InMovingTaskAsync(string sessionReference);

        Task<bool> ExistsTaskAsync(string sessionReference);
    }
}
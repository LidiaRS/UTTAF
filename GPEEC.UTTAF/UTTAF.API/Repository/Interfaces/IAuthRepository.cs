using System.Threading.Tasks;

using UTTAF.API.Models;
using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task AddAsync(AuthSessionModel model);

        Task<bool> AddAttendeeTaskAsync(AttendeeModel attendee);

        Task<bool> ExistsTaskAsync(AuthSessionModel model);

        Task<bool> RemoveTaskAsync(AuthSessionModel model);
    }
}
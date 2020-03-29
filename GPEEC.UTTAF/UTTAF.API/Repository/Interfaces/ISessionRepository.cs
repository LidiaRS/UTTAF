using System.Collections.Generic;
using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface ISessionRepository
    {
        Task AddAsync(AuthSessionModel model);

        Task<bool> AddAttendeeTaskAsync(AttendeeModel attendee);

        Task<IList<AttendeeModel>> GetAttendersTaskAsync(string reference);

        Task<bool> ExistsTaskAsync(AuthSessionModel model);

        Task<bool> ExistsTaskAsync(string reference, string password);

        Task<bool> ClearAttendeersTaskAsync(AuthSessionModel model);

        Task<bool> RemoveTaskAsync(AuthSessionModel model);
    }
}
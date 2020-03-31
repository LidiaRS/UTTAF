using System.Collections.Generic;
using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<AttendeeModel> AddAttendeeTaskAsync(AttendeeModel attendee);

        Task<IEnumerable<AttendeeModel>> GetAttendersTaskAsync(string reference);

        Task<bool> ClearAttendeersTaskAsync(AuthSessionModel model);

        Task<bool> LeaveAttendeeTaskAsync(AttendeeModel attendee);
    }
}
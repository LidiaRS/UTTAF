using System.Collections.Generic;
using System.Threading.Tasks;

using UTTAF.API.Models;

namespace UTTAF.API.Repository.Interfaces
{
	public interface IAttendeeRepository
    {
        Task<AttendeeModel> AddAttendeeTaskAsync(AttendeeModel attendee);

        Task<IEnumerable<AttendeeModel>> GetAttendersTaskAsync(string reference);

        Task<bool> ClearAttendeersTaskAsync(SessionModel model);

        Task<bool> LeaveAttendeeTaskAsync(AttendeeModel attendee);
    }
}
using System.Threading.Tasks;

using UTTAF.API.Models;

namespace UTTAF.API.Repository.Interfaces
{
	public interface IAttendeeRepository
	{
		Task<AttendeeModel> AddAttendeeTaskAsync(AttendeeModel attendee);

		Task<AttendeeModel> FindByNameInSessionTaskAsync(AttendeeModel attendee);

		Task<AttendeeModel> FindByIdInSessionTaskAsync(AttendeeModel attendee);

		Task LeaveAttendeeTaskAsync(AttendeeModel attendee);
	}
}
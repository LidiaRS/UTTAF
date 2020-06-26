using System.Threading.Tasks;

using UTTAF.API.Models;

namespace UTTAF.API.Repository.Interfaces
{
	public interface IAttendeeRepository
	{
		Task<AttendeeModel> AddAttendeeTaskAsync(AttendeeModel attendee);

		Task<AttendeeModel> FindByNameTaskAsync(AttendeeModel attendee);

		Task LeaveAttendeeTaskAsync(AttendeeModel attendee);
	}
}
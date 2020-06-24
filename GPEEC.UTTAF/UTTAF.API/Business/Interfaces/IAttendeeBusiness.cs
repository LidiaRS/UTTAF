using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business.Interfaces
{
	public interface IAttendeeBusiness
	{
		Task<AttendeeVO> JoinAtSessionTaskAsync(AttendeeVO newAttendee);

		Task LeaveAtSessionAsync(AttendeeVO attendee);

		Task<bool> ExistsAtSessionByNameTaskAsync(AttendeeVO attendee);
	}
}
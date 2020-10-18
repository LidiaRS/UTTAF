using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;

namespace UTTAF.API.Repository
{
	public class AttendeeRepository : Repository, IAttendeeRepository
	{
		public AttendeeRepository(DataContext context) : base(context)
		{
		}

		public async Task<AttendeeModel> AddAttendeeTaskAsync(AttendeeModel newAttendee)
		{
			EntityEntry<AttendeeModel> att = await _context.Attendees.AddAsync(newAttendee);
			await _context.SaveChangesAsync();

			return att.Entity;
		}

		public async Task<AttendeeModel> FindByIdInSessionTaskAsync(AttendeeModel attendeeInSession) =>
			await _context.Attendees.SingleOrDefaultAsync(attendee => attendee.AttendeeId == attendeeInSession.AttendeeId && attendee.SessionReference == attendeeInSession.SessionReference);

		public async Task<AttendeeModel> FindByNameInSessionTaskAsync(AttendeeModel attendeeData) =>
			await _context.Attendees.SingleOrDefaultAsync(attendee => attendee.Name == attendeeData.Name && attendee.SessionReference == attendeeData.SessionReference);

		public async Task LeaveAttendeeTaskAsync(AttendeeModel attendee)
		{
			_context.Attendees.Remove(attendee);
			await _context.SaveChangesAsync();
		}
	}
}
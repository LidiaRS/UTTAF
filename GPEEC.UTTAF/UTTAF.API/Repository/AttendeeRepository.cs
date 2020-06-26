using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Data.Converters;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;

namespace UTTAF.API.Repository
{
	public class AttendeeRepository : Repository, IAttendeeRepository
	{
		public AttendeeRepository(DataContext context) : base(context)
		{
		}

		public async Task<AttendeeModel> AddAttendeeTaskAsync(AttendeeModel attendee)
		{
			EntityEntry<AttendeeModel> att = await _context.Attendees.AddAsync(attendee);
			await _context.SaveChangesAsync();

			return att.Entity;
		}

		public async Task<AttendeeModel> FindByNameTaskAsync(AttendeeModel attendee) =>
			await _context.Attendees.SingleOrDefaultAsync(x => x.Name == attendee.Name && x.SessionReference == attendee.SessionReference);

		public async Task LeaveAttendeeTaskAsync(AttendeeModel attendee)
		{
			_context.Attendees.Remove(attendee);
			await _context.SaveChangesAsync();
		}
	}
}
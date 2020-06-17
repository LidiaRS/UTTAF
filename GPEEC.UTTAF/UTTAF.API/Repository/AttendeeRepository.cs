using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Collections.Generic;
using System.Linq;
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

        public async Task<AttendeeModel> AddAttendeeTaskAsync(AttendeeModel attendee)
        {
            if (await _context.Sessions.SingleOrDefaultAsync(x => x.SessionReference == attendee.SessionReference) is null)
                return default;

            if (await _context.Attendees.SingleOrDefaultAsync(x => x.SessionReference == attendee.SessionReference && x.Name == attendee.Name) is AttendeeModel)
                return default;

            EntityEntry<AttendeeModel> att = await _context.Attendees.AddAsync(attendee);
            await _context.SaveChangesAsync();

            return att.Entity;
        }

        public async Task<bool> ClearAttendeersTaskAsync(SessionModel model)
        {
            if (await GetAttendersTaskAsync(model.SessionReference) is IList<AttendeeModel> att)
            {
                _context.Attendees.RemoveRange(att);
                await _context.SaveChangesAsync();
                return true;
            }

            return default;
        }

        public async Task<IEnumerable<AttendeeModel>> GetAttendersTaskAsync(string reference) =>
            await _context.Attendees.Where(x => x.SessionReference == reference).ToListAsync();

        public async Task<bool> LeaveAttendeeTaskAsync(AttendeeModel attendee)
        {
            if (await _context.Attendees.SingleOrDefaultAsync(x => x.Id == attendee.Id && x.SessionReference == attendee.SessionReference) is AttendeeModel att)
            {
                _context.Attendees.Remove(att);
                await _context.SaveChangesAsync();
                return true;
            }

            return default;
        }
    }
}
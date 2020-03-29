using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository
{
    public class SessionRepository : Repository, ISessionRepository
    {
        public SessionRepository(DataContext context) : base(context)
        {
        }

        public async Task AddAsync(AuthSessionModel model)
        {
            await _context.Sessions.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsTaskAsync(AuthSessionModel model) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == model.SessionReference && x.SessionStatus != SessionStatusEnum.Closed);

        public async Task<bool> AddAttendeeTaskAsync(AttendeeModel attendee)
        {
            if (await _context.Sessions.SingleOrDefaultAsync(x => x.SessionReference == attendee.SessionReference) is null)
                return false;

            if (await _context.Attendees.SingleOrDefaultAsync(x => x.SessionReference == attendee.SessionReference && x.Name == attendee.Name) is AttendeeModel)
                return false;

            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ClearAttendeersTaskAsync(AuthSessionModel model)
        {
            if (await GetAttendersTaskAsync(model.SessionReference) is IList<AttendeeModel> att)
            {
                _context.Attendees.RemoveRange(att);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveTaskAsync(AuthSessionModel model)
        {
            if (await _context.Sessions.SingleOrDefaultAsync(x => x.SessionReference == model.SessionReference && x.SessionPassword == model.SessionPassword) is AuthSessionModel s)
            {
                _context.Sessions.Remove(s);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IList<AttendeeModel>> GetAttendersTaskAsync(string reference) =>
            await _context.Attendees.Where(x => x.SessionReference == reference).ToListAsync();

        public async Task<bool> ExistsTaskAsync(string reference, string password) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == reference && x.SessionPassword == password);
    }
}
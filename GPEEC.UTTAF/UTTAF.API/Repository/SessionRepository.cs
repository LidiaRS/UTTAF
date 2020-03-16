using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository
{
    public class SessionRepository : Repository, IAuthRepository
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
    }
}
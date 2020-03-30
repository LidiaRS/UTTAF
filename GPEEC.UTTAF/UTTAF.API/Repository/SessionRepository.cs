using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ExistsTaskAsync(string reference) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == reference);

        public async Task<bool> ExistsTaskAsync(string reference, string password) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == reference && x.SessionPassword == password);

        public async Task<bool> ExistsTaskAsync(AuthSessionModel model) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == model.SessionReference && x.SessionStatus != SessionStatusEnum.Closed);

        public async Task<bool> SessionStartedTaskAsync(string reference) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == reference && x.SessionStatus == SessionStatusEnum.InProgress);
    }
}
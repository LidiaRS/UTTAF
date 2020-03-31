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

        public async Task<AuthSessionModel> AddAsync(AuthSessionModel authSession)
        {
            if ((await _context.Sessions.AddAsync(authSession)).Entity is AuthSessionModel auth)
            {
                await _context.SaveChangesAsync();
                return auth;
            }

            return default;
        }

        public async Task<bool> RemoveTaskAsync(AuthSessionModel model)
        {
            if (await _context.Sessions.SingleOrDefaultAsync(x => x.SessionReference == model.SessionReference && x.SessionPassword == model.SessionPassword) is AuthSessionModel s)
            {
                _context.Sessions.Remove(s);
                await _context.SaveChangesAsync();
                return true;
            }

            return default;
        }

        public async Task<bool> ExistsTaskAsync(string reference) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == reference);

        public async Task<bool> ExistsTaskAsync(string reference, string password) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == reference && x.SessionPassword == password);

        public async Task<bool> ExistsTaskAsync(AuthSessionModel model) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == model.SessionReference && x.SessionStatus != SessionStatusEnum.Closed);

        public async Task<bool> SessionStartedTaskAsync(string reference) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == reference && x.SessionStatus == SessionStatusEnum.InProgress);

        public async Task<AuthSessionModel> ChangeStatusSessionTaskAsync(AuthSessionModel authSession)
        {
            if (await _context.Sessions.SingleOrDefaultAsync(x => x.SessionReference == authSession.SessionReference && x.SessionPassword == authSession.SessionPassword) is AuthSessionModel auth)
            {
                _context.Entry(auth).CurrentValues.SetValues(authSession);
                await _context.SaveChangesAsync();
                return auth;
            }

            return default;
        }
    }
}
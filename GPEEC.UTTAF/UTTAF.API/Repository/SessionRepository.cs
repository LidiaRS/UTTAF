using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;
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
            await _context.Sessions.AnyAsync(x => x.SessionReference == model.SessionReference);

        public Task JoinAtSessionAsync(SessionModel session) => throw new System.NotImplementedException();

        public async Task<bool> RemoveAsync(AuthSessionModel model)
        {
            if (await _context.Sessions.FirstOrDefaultAsync(x => x.SessionReference == model.SessionReference && x.SessionPassword == model.SessionPassword) is AuthSessionModel s)
            {
                _context.Sessions.Remove(s);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
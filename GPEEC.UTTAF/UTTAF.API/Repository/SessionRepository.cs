using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository
{
    public class SessionRepository : Repository, IAuthRepository
    {
        public SessionRepository(DataContext context) : base(context)
        {
        }

        public async Task AddAsync(SessionModel model)
        {
            await _context.Sessions.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsTaskAsync(SessionModel model) =>
            await _context.Sessions.AnyAsync(x => x.SessionReference == model.SessionReference);

        public async Task RemovehAsync(SessionModel model)
        {
            _context.Sessions.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
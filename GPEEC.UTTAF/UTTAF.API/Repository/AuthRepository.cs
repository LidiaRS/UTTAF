using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository
{
    public class AuthRepository : Repository, IAuthRepository
    {
        public AuthRepository(DataContext context) : base(context)
        {
        }

        public async Task AddAsync(AuthModel model)
        {
            await _context.Auths.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsTaskAsync(AuthModel model) =>
            await _context.Auths.AnyAsync(x => x.SessionReference == model.SessionReference);

        public async Task RemovehAsync(AuthModel model)
        {
            _context.Auths.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
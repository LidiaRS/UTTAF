using System;
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

        public async Task AddAuthTaskAsync(AuthModel model)
        {
            await _context.Auths.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public Task RemoveAuthTaskAsync(Guid model)
        {
            throw new NotImplementedException();
        }
    }
}
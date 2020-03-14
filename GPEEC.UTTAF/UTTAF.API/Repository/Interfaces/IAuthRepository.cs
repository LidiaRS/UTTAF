using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task AddAuthAsync(AuthModel model);

        Task<bool> ExistsAuthTaskAsync(AuthModel model);

        Task RemoveAuthAsync(Guid model);
    }
}
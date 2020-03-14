using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task AddAuthTaskAsync(AuthModel model);

        Task RemoveAuthTaskAsync(Guid model);
    }
}
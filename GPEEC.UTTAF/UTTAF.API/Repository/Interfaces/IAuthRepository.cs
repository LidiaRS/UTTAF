using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task AddAsync(AuthModel model);

        Task<bool> ExistsTaskAsync(AuthModel model);

        Task RemovehAsync(AuthModel model);
    }
}
using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task AddAsync(SessionModel model);

        Task<bool> ExistsTaskAsync(SessionModel model);

        Task RemovehAsync(SessionModel model);
    }
}
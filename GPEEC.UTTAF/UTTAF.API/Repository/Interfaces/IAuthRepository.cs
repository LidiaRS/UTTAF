using System.Threading.Tasks;

using UTTAF.API.Models;
using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task AddAsync(AuthSessionModel model);

        Task JoinAtSessionAsync(SessionModel session);

        Task<bool> ExistsTaskAsync(AuthSessionModel model);

        Task<bool> RemoveAsync(AuthSessionModel model);
    }
}
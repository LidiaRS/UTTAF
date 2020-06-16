using System.Threading.Tasks;

using UTTAF.API.Models;

namespace UTTAF.API.Repository.Interfaces
{
	public interface ISessionRepository
    {
        Task<AuthSessionModel> AddAsync(AuthSessionModel authSession);

        Task<bool> ExistsTaskAsync(AuthSessionModel authSession);

        Task<bool> ExistsTaskAsync(string reference, string password);

        Task<bool> ExistsTaskAsync(string reference);

        Task<bool> SessionStartedTaskAsync(string reference);

        Task<bool> RemoveTaskAsync(AuthSessionModel authSession);

        Task<AuthSessionModel> ChangeStatusSessionTaskAsync(AuthSessionModel authSession);
    }
}
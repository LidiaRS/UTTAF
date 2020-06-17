using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business
{
	public interface ISessionBusiness
	{
		Task<AuthSessionVO> AddSessionTaskAsync(AuthSessionVO authSession);

		Task<bool> ExistsBySessionReferenceTaskAsync(string reference);

		Task<bool> ExistsBySessionReferenceAndPasswordTaskAsync(string reference, string password);

		Task<bool> ExistsByAuthSessionTaskAsync(AuthSessionVO authSession);

		Task<bool> SessionStartedTaskAsync(string reference);

		Task<bool> RemoveTaskAsync(AuthSessionVO authSession);

		Task<AuthSessionVO> ChangeStatusSessionTaskAsync(AuthSessionVO authSession);
	}
}
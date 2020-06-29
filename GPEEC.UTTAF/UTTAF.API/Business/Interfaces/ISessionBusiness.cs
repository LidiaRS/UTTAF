using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business
{
	public interface ISessionBusiness
	{
		Task<SessionVO> AddSessionTaskAsync(SessionVO session);

		Task<SessionVO> FindBySessionReferenceTaskAsync(string sessionReference);

		Task<SessionVO> ChangeStatusSessionTaskAsync(SessionVO session);

		Task<bool> RemoveTaskAsync(string sessionReference);
	}
}
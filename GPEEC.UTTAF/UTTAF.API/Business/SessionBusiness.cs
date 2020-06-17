using System;
using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business
{
	public class SessionBusiness : ISessionBusiness
	{
		public Task<AuthSessionVO> AddSessionTaskAsync(AuthSessionVO authSession) => throw new NotImplementedException();

		public Task<AuthSessionVO> ChangeStatusSessionTaskAsync(AuthSessionVO authSession) => throw new NotImplementedException();

		public Task<bool> ExistsByAuthSessionTaskAsync(AuthSessionVO authSession) => throw new NotImplementedException();

		public Task<bool> ExistsBySessionReferenceAndPasswordTaskAsync(string reference, string password) => throw new NotImplementedException();

		public Task<bool> ExistsBySessionReferenceTaskAsync(string reference) => throw new NotImplementedException();

		public Task<bool> RemoveTaskAsync(AuthSessionVO authSession) => throw new NotImplementedException();

		public Task<bool> SessionStartedTaskAsync(string reference) => throw new NotImplementedException();
	}
}
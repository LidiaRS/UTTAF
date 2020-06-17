using System;
using System.Threading.Tasks;

using UTTAF.API.Data.Converters;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business
{
	public class SessionBusiness : ISessionBusiness
	{
		private readonly ISessionRepository _sessionRepository;
		private readonly SessionConverter _sessionConverter;

		public SessionBusiness(ISessionRepository sessionRepository, SessionConverter sessionConverter)
		{
			_sessionRepository = sessionRepository;
			_sessionConverter = sessionConverter;
		}

		public Task<SessionVO> AddSessionTaskAsync(SessionVO session) => throw new NotImplementedException();

		public async Task<SessionVO> FindBySessionReferenceTaskAsync(string sessionReference) =>
			_sessionConverter.Parse(await _sessionRepository.FindBySessionReferenceTaskAsync(sessionReference));

		public async Task<SessionVO> ChangeStatusSessionTaskAsync(SessionVO newSession)
		{
			if (!(await _sessionRepository.FindBySessionReferenceTaskAsync(newSession.SessionReference) is SessionModel currentSession))
				return null;

			newSession.SessionDate = currentSession.SessionDate;

			return _sessionConverter.Parse(await _sessionRepository.ChangeStatusSessionTaskAsync(currentSession, _sessionConverter.Parse(newSession)));
		}

		public async Task<bool> RemoveTaskAsync(SessionVO session)
		{
			if (!(await _sessionRepository.FindBySessionReferenceTaskAsync(session.SessionReference) is SessionModel authSessionModel))
				return false;

			await _sessionRepository.RemoveTaskAsync(authSessionModel);
			return true;
		}
	}
}
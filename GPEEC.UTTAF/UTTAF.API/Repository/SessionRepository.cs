using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UTTAF.API.Data;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;

namespace UTTAF.API.Repository
{
	public class SessionRepository : Repository, ISessionRepository
	{
		public SessionRepository(DataContext context) : base(context)
		{
		}

		public async Task<SessionModel> AddAsync(SessionModel session)
		{
			SessionModel addedSession = (await _context.Sessions.AddAsync(session)).Entity;
			await _context.SaveChangesAsync();
			return addedSession;
		}

		public async Task<SessionModel> FindBySessionReferenceTaskAsync(string sessionReference) =>
			await _context.Sessions.SingleOrDefaultAsync(x => x.SessionReference == sessionReference);

		public async Task<SessionModel> ChangeStatusSessionTaskAsync(SessionModel currentSession, SessionModel newSession)
		{
			_context.Entry(currentSession).CurrentValues.SetValues(newSession);
			await _context.SaveChangesAsync();
			return newSession;
		}

		public async Task RemoveTaskAsync(SessionModel session)
		{
			_context.Sessions.Remove(session);
			await _context.SaveChangesAsync();
		}
	}
}
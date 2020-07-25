using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.API.Business;
using UTTAF.API.Business.Interfaces;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Interfaces.RPC.Clients;
using UTTAF.Dependencies.Interfaces.RPC.Hubs;

namespace UTTAF.API.Hubs
{
	public class SessionHub : Hub<ISessionAttendeeClients>, ISessionHub, IAttendeeHub
	{
		private readonly ISessionBusiness _sessionBusiness;
		private readonly IAttendeeBusiness _attendeeBusiness;

		public SessionHub(ISessionBusiness sessionBusiness, IAttendeeBusiness attendeeBusiness)
		{
			_sessionBusiness = sessionBusiness;
			_attendeeBusiness = attendeeBusiness;
		}

		public async Task CreateSessionAsync(SessionVO newSession)
		{
			if (await _sessionBusiness.FindBySessionReferenceTaskAsync(newSession.SessionReference) is SessionVO)
			{
				await Clients.Caller.AlreadyExistsSessionAsync("Já existe uma sessao com este nome!");
				return;
			}

			if (!(await _sessionBusiness.AddSessionTaskAsync(newSession) is SessionVO addedSession))
			{
				await Clients.Caller.NotCreatedSessionAsync("Nao foi possível criar a sessao, tente novamente!");
				return;
			}

			await Clients.Caller.CreatedSessionAsync(addedSession, "Sessao criada com sucesso!");
		}

		public async Task MarkSessionWithStartedAsync(SessionVO newSession)
		{
			if (!(await _sessionBusiness.FindBySessionReferenceTaskAsync(newSession.SessionReference) is SessionVO currentSession))
			{
				await Clients.Caller.NotExistsThisSessionAsync("Nao existe uma sessao com esse nome!");
				return;
			}

			if (currentSession.SessionStatus == SessionStatusEnum.InProgress)
			{
				await Clients.Caller.NotUpdatedSessionStatusAsync("A sessao ja está em andamento!");
				return;
			}

			if (!(await _sessionBusiness.ChangeStatusSessionTaskAsync(newSession) is SessionVO updatedSession))
			{
				await Clients.Caller.NotUpdatedSessionStatusAsync("Nao foi possível iniciar a sessao, tente novamente!");
				return;
			}

			await Clients.Caller.UpdatedSessionStatusAsync(updatedSession, "Sessao iniciada com sucesso!");
		}

		public async Task DeleteSessionAsync(string sessionReference)
		{
			if (!(await _sessionBusiness.FindBySessionReferenceTaskAsync(sessionReference) is SessionVO))
			{
				await Clients.Caller.NotExistsThisSessionAsync("Nao existe uma sessao com esse nome!");
				return;
			}

			if (!await _sessionBusiness.RemoveTaskAsync(sessionReference))
			{
				await Clients.Caller.NotRemovedSessionAsync("Nao foi possivel remover a sessao, tente novamente!");
				return;
			}

			await Clients.Caller.RemovedSessionAsync("Sessao removida com sucesso!");
		}

		public async Task JoinAtSessionAsync(AttendeeVO newAttendee)
		{
			if (!(await _sessionBusiness.FindBySessionReferenceTaskAsync(newAttendee.SessionReference) is SessionVO session))
			{
				await Clients.Caller.NotExistsThisSessionAsync("Nao existe uma sessao com esse nome!");
				return;
			}

			if (await _attendeeBusiness.FindByNameInSessionTaskAsync(newAttendee) is AttendeeVO)
			{
				await Clients.Caller.NotExistsAttendeeWithThisNameAsync("Ja existe um participante com este nome!");
				return;
			}

			if (!(await _attendeeBusiness.JoinAtSessionTaskAsync(newAttendee) is AttendeeVO joinedAttendee))
			{
				await Clients.Caller.NotJoinedAtSessionAsync("Nao foi possivel participar da sessao, tente novamente!");
				return;
			}

			await Clients.Caller.JoinedAtSessionAsync(joinedAttendee, session, "Agora voce está participando da sessao!");
			await Clients.OthersInGroup(joinedAttendee.SessionReference).AttendeeExitedAsync(joinedAttendee, $"{joinedAttendee.Name} agora está participando da sessao!");
			await Groups.AddToGroupAsync(Context.ConnectionId, session.SessionReference);
		}

		public async Task LeaveAtSessionAsync(AttendeeVO attendee)
		{
			if (!(await _sessionBusiness.FindBySessionReferenceTaskAsync(attendee.SessionReference) is SessionVO))
			{
				await Clients.Caller.NotExistsThisSessionAsync("Nao existe uma sessao com esse nome!");
				return;
			}

			if (!(await _attendeeBusiness.FindByNameInSessionTaskAsync(attendee) is AttendeeVO currentAttendee))
			{
				await Clients.Caller.NotExistsAttendeeWithThisNameAsync("Nao existe nenhum participante na sessao com este nome!");
				return;
			}

			await _attendeeBusiness.LeaveAtSessionAsync(currentAttendee);
			await Clients.Caller.ExitedAtSessionAsync("Voce deixou a sessao!");
			await Clients.OthersInGroup(currentAttendee.SessionReference).AttendeeExitedAsync(currentAttendee, $"{currentAttendee.Name} deixou a sessao!");
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, currentAttendee.SessionReference);
		}
	}
}
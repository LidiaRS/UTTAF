﻿using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.API.Business;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Interfaces.Clients;
using UTTAF.Dependencies.Interfaces.Hubs;

namespace UTTAF.API.Hubs
{
	public class SessionHub : Hub<ISessionClient>, ISessionHub
	{
		private readonly ISessionBusiness _sessionBusiness;

		public SessionHub(ISessionBusiness sessionBusiness)
		{
			_sessionBusiness = sessionBusiness;
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

			if(currentSession.SessionStatus == SessionStatusEnum.InProgress)
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

		public async Task DeleteSessionAsync(SessionVO session)
		{
			if (!(await _sessionBusiness.FindBySessionReferenceTaskAsync(session.SessionReference) is SessionVO))
			{
				await Clients.Caller.NotExistsThisSessionAsync("Nao existe uma sessao com esse nome!");
				return;
			}

			if (!await _sessionBusiness.RemoveTaskAsync(session))
			{
				await Clients.Caller.NotRemovedSessionAsync("Nao foi possivel remover a sessao, tente novamente!");
				return;
			}

			await Clients.Caller.RemovedSessionAsync("Sessao removida com sucesso!");
		}
	}
}
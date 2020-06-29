﻿using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Interfaces.RPC.Hubs
{
	public interface ISessionHub
	{
		Task CreateSessionAsync(SessionVO newSession);

		Task MarkSessionWithStartedAsync(SessionVO newSession);

		Task DeleteSessionAsync(string sessionReference);
	}
}
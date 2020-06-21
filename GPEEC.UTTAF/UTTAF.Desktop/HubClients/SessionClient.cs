using Microsoft.AspNetCore.SignalR.Client;

using System.Windows;

using UTTAF.Dependencies.Data.Clients.Interfaces;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Services.HubConnections;

namespace UTTAF.Desktop.HubClients
{
	public class SessionClient
	{
		private readonly SessionConnection _sessionConnection;

		public SessionClient(SessionConnection sessionConnection)
		{
			_sessionConnection = sessionConnection;

			_sessionConnection.Connection.On<SessionVO, string>(nameof(ISessionClient.CreatedSessionAsync), (session, message) =>
			{
				MessageBox.Show($"{message}\n{session}");
			});
		}
	}
}
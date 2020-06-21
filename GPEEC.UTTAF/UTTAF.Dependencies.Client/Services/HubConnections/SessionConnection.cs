using Microsoft.AspNetCore.SignalR.Client;

using UTTAF.Dependencies.Helpers;

namespace UTTAF.Dependencies.Clients.Services.HubConnections
{
	public class SessionConnection
	{
		public HubConnection Connection { get; private set; }

		public SessionConnection()
		{
			Connection = new HubConnectionBuilder().WithUrl($"{DataHelper.URLBase}/session").WithAutomaticReconnect().Build();
		}
	}
}
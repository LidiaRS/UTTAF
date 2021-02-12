using Microsoft.AspNetCore.SignalR.Client;

namespace UTTAF.Dependencies.Clients.Services.HubConnections
{
	public class SessionConnection
	{
		public HubConnection Connection { get; private set; }

		public SessionConnection(string url)
		{
			IHubConnectionBuilder hubConnectionBuilder = new HubConnectionBuilder().WithUrl($"{url}/session");
			Connection = hubConnectionBuilder.WithAutomaticReconnect().Build();
		}
	}
}
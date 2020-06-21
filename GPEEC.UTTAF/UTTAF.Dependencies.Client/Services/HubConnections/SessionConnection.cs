using Microsoft.AspNetCore.SignalR.Client;

using System.Threading.Tasks;

using UTTAF.Dependencies.Clients.Helpers;

namespace UTTAF.Dependencies.Clients.Services.HubConnections
{
	public class SessionConnection
	{
		public HubConnection Connection { get; private set; }

		public SessionConnection()
		{
			IHubConnectionBuilder hubConnectionBuilder = new HubConnectionBuilder().WithUrl($"{DataHelper.URL}/session");
			Connection = hubConnectionBuilder.WithAutomaticReconnect().Build();
			Task.Run(async () => await Connection.StartAsync());
		}
	}
}
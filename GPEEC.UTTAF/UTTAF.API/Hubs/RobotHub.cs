using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

using UTTAF.API.Models;
using UTTAF.API.Models.Auxiliary;

namespace UTTAF.API.Hubs
{
	public class RobotHub : Hub
	{
		public RobotHub()
		{
		}

		public async Task NewRobotAsync(RobotModel newRobot, string sessionPassword) { }

		public async Task FoundCardAsync(CardModel card) { }
	}
}
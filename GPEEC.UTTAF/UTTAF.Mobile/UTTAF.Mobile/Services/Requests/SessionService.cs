using Dependencies;
using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Mobile.Services.Requests
{
	internal class SessionService
	{
		internal static async Task<IRestResponse> SessionStartedTaskAsync(string reference)
		{
			var request = new RequestService()
			{
				Protocol = Protocols.HTTP,
				URL = DataHelper.URL,
				URN = "Session/Started",
				Method = Method.GET
			};

			request.Parameters.Add(nameof(SessionVO.SessionReference), reference);

			return await request.ExecuteTaskAsync();
		}
	}
}
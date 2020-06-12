using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Desktop.Services.Requests
{
	public class AttendeeService
	{
		public static async Task<IRestResponse> GetAttendeesTaskAsync(AuthSessionModel model)
		{
			var request = new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = "Attendee",
				Method = Method.GET
			};

			request.Parameters.Add(nameof(model.SessionReference), model.SessionReference);
			request.Parameters.Add(nameof(model.SessionPassword), model.SessionPassword);

			return await request.ExecuteTaskAsync();
		}
	}
}
using Dependencies;
using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Helpers;

namespace UTTAF.Mobile.Services.Requests
{
	internal class AttendeeService
	{
		internal async static Task<IRestResponse> JoinAtSessionTaskAsync(AttendeeVO attendee)
		{
			return await new RequestService()
			{
				Protocol = Protocols.HTTP,
				URL = DataHelper.URLBase,
				URN = "Attendee/Join",
				Body = attendee,
				Method = Method.POST
			}.ExecuteTaskAsync();
		}

		internal async static Task<IRestResponse> LeaveAtSessionTaskAsync(AttendeeVO attendee)
		{
			return await new RequestService()
			{
				Protocol = Protocols.HTTP,
				URL = DataHelper.URLBase,
				URN = "Attendee/Leave",
				Body = attendee,
				Method = Method.DELETE
			}.ExecuteTaskAsync();
		}
	}
}
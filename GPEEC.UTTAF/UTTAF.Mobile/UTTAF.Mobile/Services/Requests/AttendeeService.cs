using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Mobile.Services.Requests
{
    internal class AttendeeService
    {
        internal async static Task<IRestResponse> JoinAtSessionTaskAsync(AttendeeModel attendee)
        {
            return await new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Attendee/Join",
                Body = attendee,
                Method = Method.POST
            }.ExecuteTaskAsync();
        }

        internal async static Task<IRestResponse> LeaveAtSessionTaskAsync(AttendeeModel attendee)
        {
            return await new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Attendee/Leave",
                Body = attendee,
                Method = Method.DELETE
            }.ExecuteTaskAsync();
        }
    }
}
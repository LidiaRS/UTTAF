using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Helpers;

namespace UTTAF.Desktop.Services.Requests
{
	public class SessionService
	{
		public async Task<IRestResponse> InitSessionTaskAsync(AuthSessionVO authSession)
		{
			return await new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = "Session",
				Method = Method.POST,
				Body = authSession
			}.ExecuteTaskAsync();
		}

		public async Task<IRestResponse> StartSessionTaskAsync(AuthSessionVO authSession)
		{
			return await new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = "Session/Status",
				Method = Method.PUT,
				Body = authSession
			}.ExecuteTaskAsync();
		}

		public async Task<IRestResponse> DeleteSessionTaskAsync(AuthSessionVO model)
		{
			return await new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = "Session",
				Method = Method.DELETE,
				Body = model
			}.ExecuteTaskAsync();
		}
	}
}
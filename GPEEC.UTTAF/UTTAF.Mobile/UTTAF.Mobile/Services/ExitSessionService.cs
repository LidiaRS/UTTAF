using Logikoz.XamarinUtilities.Utilities;

using RestSharp;

using System.Net;
using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile.Services
{
	internal class ExitSessionService
	{

		public async Task<bool> ExitTaskAsync(AttendeeVO attendee)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				await Application.Current.MainPage.DisplayAlert("Concluido!", "Voce deixou a sessao 😥", "OK");
				ClosePages();
			}
			else if (ReturnExpression(response))
			{
				await SendAlertMessageAsync(response);
				return false;
			}
			else if (response.StatusCode == HttpStatusCode.NotFound)
			{
				await SendAlertMessageAsync(response);
				ClosePages();
			}

			return true;

			static bool ReturnExpression(IRestResponse response) =>
				response.StatusCode == HttpStatusCode.Conflict || response.StatusCode == HttpStatusCode.BadRequest;

			static async Task SendAlertMessageAsync(IRestResponse response) =>
				await Application.Current.MainPage.DisplayAlert("Ops!", response.Content.Replace("\"", string.Empty), "OK");

			static void ClosePages()
			{
				PopPushViewUtil.PopModal<JoinedSessionView>(true);
				PopPushViewUtil.PopModal<JoinSessionView>(false);
			}
		}
	}
}
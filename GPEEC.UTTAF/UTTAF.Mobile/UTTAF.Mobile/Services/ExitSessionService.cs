using Logikoz.XamarinUtilities.Utilities;

using RestSharp;

using System.Net;
using System.Threading.Tasks;

using UTTAF.Dependencies.Models;
using UTTAF.Mobile.Services.Requests;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile.Services
{
    internal class ExitSessionService
    {
        public static async Task ExitAsync(AttendeeModel attendee)
        {
            IRestResponse response = await AttendeeService.LeaveAtSessionTaskAsync(attendee);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                await Application.Current.MainPage.DisplayAlert("Concluido!", "Voce deixou a sessao 😥", "OK");
                ClosePages();
            }
            else if (ReturnExpression(response))
                await SendAlertMessageAsync(response);
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                await SendAlertMessageAsync(response);
                ClosePages();
            }

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
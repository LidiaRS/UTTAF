using Logikoz.XamarinUtilities.Utilities;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Mobile.Services.Requests;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile.Services
{
    internal class JoinAtSessionService
    {
        internal static async Task JoinAsync(AttendeeModel attendee)
        {
            IRestResponse response = await AttendeeService.JoinAtSessionTaskAsync(attendee);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                DataHelper.Attendee = JsonSerializer.Deserialize<AttendeeModel>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                await PopPushViewUtil.PushModalAsync(new JoinedSessionView(), true);
            }
            else if (ReturnExpression(response))
                await Application.Current.MainPage.DisplayAlert("Ops!", response.Content.Replace("\"", string.Empty), "OK");

            static bool ReturnExpression(IRestResponse response) =>
                response.StatusCode == HttpStatusCode.Conflict || response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients.Data.Converters;
using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Clients.Models;
using UTTAF.Dependencies.Clients.Utils;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile.ViewModels
{
	public class JoinedSessionViewModel : PropertyNotifier
	{
		private readonly AttendeeHubService _attendeeHubService;
		private readonly AttendeeConverter _attendeeConverter;
		private AttendeeModel __attendee;

		public AttendeeModel Attendee
		{
			get => __attendee;
			set => Set(ref __attendee, value);
		}

		public ICommand ExitSessionCommand { get; private set; }

		public JoinedSessionViewModel(AttendeeHubService attendeeHubService, AttendeeConverter attendeeConverter)
		{
			_attendeeHubService = attendeeHubService;
			_attendeeConverter = attendeeConverter;

			Initialize();
		}

		private void Initialize()
		{
			Attendee = DataHelper.Attendee;

			_attendeeHubService.UpdatedSessionStatus((session, message) => Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
			{
				Application.Current.MainPage = ((App)Application.Current).ServiceProvider.GetRequiredService<MovingRobotView>();
			}));

			_attendeeHubService.NotUpdatedSessionStatus(message => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert("Ops!", message, "Ok");
			}));

			_attendeeHubService.ExitedAtSession(message => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert("Até logo!", message, "Ok");
				await Application.Current.MainPage.Navigation.PopModalAsync(true);
			}));

			//commands
			ExitSessionCommand = new Command(async () => await ExitSessionAsync());
		}

		private async Task ExitSessionAsync()
		{
			await _attendeeHubService.LeaveAtSessionAsync(_attendeeConverter.Parse(Attendee));
		}
	}
}
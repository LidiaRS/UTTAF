using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Clients.ViewModels;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile.ViewModels
{
	public class JoinedSessionViewModel : ViewModel
	{
		private readonly AttendeeHubService _attendeeHubService;

		private AttendeeVO __attendee;

		public AttendeeVO Attendee
		{
			get => __attendee;
			set => Set(ref __attendee, value);
		}

		public ICommand ExitSessionCommand { get; private set; }

		public JoinedSessionViewModel(AttendeeHubService attendeeHubService)
		{
			_attendeeHubService = attendeeHubService;

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

			//commands
			ExitSessionCommand = new Command(async () => await ExitSessionAsync());
		}

		private async Task ExitSessionAsync()
		{
			await _attendeeHubService.LeaveAtSessionAsync(Attendee);
		}
	}
}
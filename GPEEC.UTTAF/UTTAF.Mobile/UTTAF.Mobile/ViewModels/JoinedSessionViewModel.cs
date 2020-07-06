using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients;
using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile.ViewModels
{
	public class JoinedSessionViewModel : ViewModelBase
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

			_attendeeHubService.UpdatedSessionStatus((session, message) =>
			{
				Application.Current.MainPage = ((App)Application.Current).ServiceProvider.GetRequiredService<MovingRobotView>();
			});

			_attendeeHubService.NotUpdatedSessionStatus(async message =>
			{
				await Application.Current.MainPage.DisplayAlert("Ops!", message, "Ok");
			});

			//commands
			ExitSessionCommand = new Command(async () => await ExitSessionAsync());
		}

		private async Task ExitSessionAsync()
		{
			await _attendeeHubService.LeaveAtSessionAsync(Attendee);
		}
	}
}
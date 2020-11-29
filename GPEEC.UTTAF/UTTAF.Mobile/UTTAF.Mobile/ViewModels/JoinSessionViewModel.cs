using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients.Data.Converters;
using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Clients.Models;
using UTTAF.Dependencies.Clients.Utils;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Services.Interfaces;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

using ZXing;

namespace UTTAF.Mobile.ViewModels
{
	public class JoinSessionViewModel : PropertyNotifier
	{
		private readonly IBarCodeService _barCodeService;
		private readonly AttendeeHubService _attendeeHubService;
		private readonly SessionHubService _sessionHubService;
		private readonly AttendeeConverter _attendeeConverter;
		private AttendeeModel __attendee;

		public AttendeeModel Attendee
		{
			get => __attendee;
			set => Set(ref __attendee, value);
		}

		public ICommand JoinAtSessionWithQrCodeCommand { get; private set; }
		public ICommand JoinAtSessionCommand { get; private set; }

		public JoinSessionViewModel(IBarCodeService barCodeService, AttendeeHubService attendeeHubService, SessionHubService sessionHubService, AttendeeConverter attendeeConverter)
		{
			_barCodeService = barCodeService;
			_attendeeHubService = attendeeHubService;
			_sessionHubService = sessionHubService;
			_attendeeConverter = attendeeConverter;

			Initialize();
		}

		private void Initialize()
		{
			Attendee = new AttendeeModel();

			//commands
			JoinAtSessionWithQrCodeCommand = new Command(async () => await JoinAtSessionWithQrCodeAsync());
			JoinAtSessionCommand = new Command(async () => await JoinAtSessionAsync());

			_attendeeHubService.JoinedAtSession((attendee, session, message) => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				//TODO: refatorar isso para um service
				DataHelper.Attendee = _attendeeConverter.Parse(attendee);
				DataHelper.AuthSession = session;
				await Application.Current.MainPage.Navigation.PushModalAsync(((App)Application.Current).ServiceProvider.GetRequiredService<JoinedSessionView>(), true);
			}));

			_attendeeHubService.NotJoinedAtSession(message => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert("Ops!", message, "OK");
			}));

			_attendeeHubService.NotExistsAttendeeWithThisName(message => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert("Ops!", message, "OK");
			}));

			_sessionHubService.DeletedSession(message => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert("Ops!", message, "Ok");
				await Application.Current.MainPage.Navigation.PopModalAsync(true);
			}));
		}

		private async Task JoinAtSessionAsync()
		{
			await _attendeeHubService.JoinAtSessionAsync(_attendeeConverter.Parse(Attendee));
		}

		private async Task JoinAtSessionWithQrCodeAsync()
		{
			Result qrCodeScannedResult = await _barCodeService.ScanQrCodeTaskAsync();

			if (qrCodeScannedResult != null)
			{
				Attendee.SessionReference = qrCodeScannedResult.Text;
				await _attendeeHubService.JoinAtSessionAsync(_attendeeConverter.Parse(Attendee));
			}
		}
	}
}
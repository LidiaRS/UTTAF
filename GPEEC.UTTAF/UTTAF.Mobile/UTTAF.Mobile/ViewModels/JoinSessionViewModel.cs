using Logikoz.XamarinUtilities.Utilities;

using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Clients.ViewModels;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Services.Interfaces;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

using ZXing;

namespace UTTAF.Mobile.ViewModels
{
	public class JoinSessionViewModel : ViewModel
	{
		private readonly IBarCodeService _barCodeService;
		private readonly AttendeeHubService _attendeeHubService;

		private AttendeeVO __attendee;

		public AttendeeVO Attendee
		{
			get => __attendee;
			set => Set(ref __attendee, value);
		}

		public ICommand JoinAtSessionWithQrCodeCommand { get; private set; }
		public ICommand JoinAtSessionCommand { get; private set; }

		public JoinSessionViewModel(IBarCodeService barCodeService, AttendeeHubService attendeeHubService)
		{
			_barCodeService = barCodeService;
			_attendeeHubService = attendeeHubService;

			Initialize();
		}

		private void Initialize()
		{
			Attendee = new AttendeeVO();

			//commands
			JoinAtSessionWithQrCodeCommand = new Command(async () => await JoinAtSessionWithQrCodeAsync());
			JoinAtSessionCommand = new Command(async () => await JoinAtSessionAsync());

			_attendeeHubService.JoinedAtSession((attendee, session, message) => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				//TODO: refatorar isso para um service
				DataHelper.Attendee = attendee;
				DataHelper.AuthSession = session;
				await PopPushViewUtil.PushModalAsync(((App)Application.Current).ServiceProvider.GetRequiredService<JoinedSessionView>(), true);
			}));

			_attendeeHubService.NotJoinedAtSession(message => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert("Ops!", message, "OK");
			}));

			_attendeeHubService.NotExistsAttendeeWithThisName(message => Application.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert("Ops!", message, "OK");
			}));
		}

		private async Task JoinAtSessionAsync()
		{
			await _attendeeHubService.JoinAtSessionAsync(Attendee);
		}

		private async Task JoinAtSessionWithQrCodeAsync()
		{
			Result qrCodeScannedResult = await _barCodeService.ScanQrCodeTaskAsync();

			if (qrCodeScannedResult != null)
			{
				Attendee.SessionReference = qrCodeScannedResult.Text;
				await _attendeeHubService.JoinAtSessionAsync(Attendee);
			}
		}
	}
}
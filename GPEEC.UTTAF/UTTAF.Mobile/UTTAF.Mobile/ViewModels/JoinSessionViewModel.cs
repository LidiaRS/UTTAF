using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Services.Interfaces;

using Xamarin.Forms;

using ZXing;

namespace UTTAF.Mobile.ViewModels
{
	internal class JoinSessionViewModel : ViewModelBase
	{
		private readonly IBarCodeService _barCodeService;

		private string __sessionReference;
		private string __attendee;

		public string SessionReference
		{
			get => __sessionReference;
			set => Set(ref __sessionReference, value);
		}

		public string Attendee
		{
			get => __attendee;
			set => Set(ref __attendee, value);
		}

		public ICommand JoinAtSessionWithQrCodeCommand { get; private set; }
		public ICommand JoinAtSessionCommand { get; private set; }

		public JoinSessionViewModel(IBarCodeService barCodeService)
		{
			_barCodeService = barCodeService;

			Initialize();
		}

		private void Initialize()
		{
			//commands
			JoinAtSessionWithQrCodeCommand = new Command(async () => await JoinAtSessionWithQrCodeAsync());
			JoinAtSessionCommand = new Command(async () => await JoinAtSessionAsync());
		}

		private async Task JoinAtSessionAsync()
		{
			await JoinAtSessionService.JoinAsync(new AttendeeVO { SessionReference = SessionReference, Name = Attendee });
		}

		private async Task JoinAtSessionWithQrCodeAsync()
		{
			Result qrCodeScannedResult = await _barCodeService.ScanQrCodeTaskAsync();

			if (qrCodeScannedResult != null)
				await JoinAtSessionService.JoinAsync(new AttendeeVO { SessionReference = SessionReference = qrCodeScannedResult.Text, Name = Attendee });
		}
	}
}
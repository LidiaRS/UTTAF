using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Mobile.Services;

using Xamarin.Forms;

using ZXing;
using ZXing.Mobile;

namespace UTTAF.Mobile.ViewModels
{
	internal class JoinSessionViewModel : ViewModelBase
	{
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

		public JoinSessionViewModel() => Init();

		private void Init()
		{
			//commands
			JoinAtSessionWithQrCodeCommand = new Command(async () => await JoinAtSessionWithQrCodeAsync());
			JoinAtSessionCommand = new Command(async () => await JoinAtSessionAsync());
		}

		private async Task JoinAtSessionAsync() =>
			await JoinAtSessionService.JoinAsync(new AttendeeVO { SessionReference = SessionReference, Name = Attendee });

		private async Task JoinAtSessionWithQrCodeAsync()
		{
			Result result = await new MobileBarcodeScanner()
			{
				BottomText = "Aponte para o QrCode",
				TopText = "UTTAF",
				CameraUnsupportedMessage = "Camera não suportada!"
			}.Scan(new MobileBarcodeScanningOptions
			{
				AutoRotate = false,
				PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
			});

			if (result != null)
				await JoinAtSessionService.JoinAsync(new AttendeeVO { SessionReference = SessionReference = result.Text, Name = Attendee });
		}
	}
}
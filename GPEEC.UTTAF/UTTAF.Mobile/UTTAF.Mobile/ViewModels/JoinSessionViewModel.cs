using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System.Collections.Generic;

using UTTAF.Dependencies.Models;
using UTTAF.Mobile.Services;

using ZXing;
using ZXing.Mobile;

namespace UTTAF.Mobile.ViewModels
{
    internal class JoinSessionViewModel : ViewModelBase
    {
        private string _sessionReference;
        private string _attendee;

        public string SessionReference
        {
            get => _sessionReference;
            set => Set(ref _sessionReference, value);
        }

        public string Attendee
        {
            get => _attendee;
            set => Set(ref _attendee, value);
        }

        public RelayCommand JoinAtSessionWithQrCodeCommand { get; private set; }
        public RelayCommand JoinAtSessionCommand { get; private set; }

        public JoinSessionViewModel() => Init();

        private void Init()
        {
            //commands
            JoinAtSessionWithQrCodeCommand = new RelayCommand(JoinAtSessionWithQrCode);
            JoinAtSessionCommand = new RelayCommand(JoinAtSession);
        }

        private async void JoinAtSession() =>
            await JoinAtSessionService.JoinAsync(new AttendeeModel { SessionReference = SessionReference, Name = Attendee });

        private async void JoinAtSessionWithQrCode()
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
                await JoinAtSessionService.JoinAsync(new AttendeeModel { SessionReference = SessionReference = result.Text, Name = Attendee });
        }
    }
}
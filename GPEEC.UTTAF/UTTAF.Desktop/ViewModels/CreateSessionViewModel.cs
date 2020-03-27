using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using QRCoder;

using RestSharp;

using System.Net;
using System.Windows;

using UTTAF.Dependencies.Helpers;
using UTTAF.Desktop.Services.Requests;
using UTTAF.Desktop.Views;

namespace UTTAF.Desktop.ViewModels
{
    internal class CreateSessionViewModel : ViewModelBase
    {
        private string _sessionReference;
        private object _qrCodeImg;

        public object QrCode
        {
            get => _qrCodeImg;
            set => Set(ref _qrCodeImg, value);
        }

        public string SessionReference
        {
            get => _sessionReference;
            set => Set(ref _sessionReference, value);
        }

        public RelayCommand<CreateSessionView> CancelSessionCreationCommand { get; private set; }

        public CreateSessionViewModel()
        {
            CancelSessionCreationCommand = new RelayCommand<CreateSessionView>(CancelSessionCreation);
        }

        public void Init()
        {
            SessionReference = DataHelper.AuthSession.SessionReference;
            QrCode = GenerateQrCode();
        }

        private async void CancelSessionCreation(CreateSessionView sessionView)
        {
            IRestResponse response = await SessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);

            if (response.StatusCode == HttpStatusCode.OK)
                sessionView.CancelSession();
            else if (response.StatusCode == HttpStatusCode.NotFound)
                MessageBox.Show(response.Content.Replace("\"", string.Empty));
        }

        private object GenerateQrCode()
        {
            using var generator = new QRCodeGenerator();
            using QRCodeData data = generator.CreateQrCode(SessionReference, QRCodeGenerator.ECCLevel.Q);
            using var qRCode = new QRCode(data);
            return qRCode.GetGraphic(10, System.Drawing.Color.Black, System.Drawing.Color.White, false);
        }
    }
}
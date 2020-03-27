using GalaSoft.MvvmLight;

using QRCoder;

using UTTAF.Dependencies.Helpers;

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

        public void Init()
        {
            SessionReference = DataHelper.AuthSession.SessionReference;
            QrCode = GenerateQrCode();
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
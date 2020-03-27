using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using QRCoder;

using System.Windows.Media;

using UTTAF.Dependencies.Helpers;

namespace UTTAF.Desktop.ViewModels
{
    internal class ConfigureViewModel : ViewModelBase
    {
        private string _reference;

        public ImageSource QrCode { get; private set; }

        public string Reference
        {
            get => _reference;
            set => Set(ref _reference, value);
        }

        public RelayCommand Command { get; private set; }

        public ConfigureViewModel() => Init();

        private void Init()
        {
        }

        private void GenerateQrCode()
        {
            using var generator = new QRCodeGenerator();
            using QRCodeData data = generator.CreateQrCode(DataHelper.AuthSession.SessionReference, QRCodeGenerator.ECCLevel.Q);
            using var qRCode = new QRCode(data);
            qRCode.GetGraphic(10, System.Drawing.Color.Black, System.Drawing.Color.White, false);
        }
    }
}
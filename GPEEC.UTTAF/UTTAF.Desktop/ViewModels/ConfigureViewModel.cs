using GalaSoft.MvvmLight;

using QRCoder;

using System.Drawing;

using UTTAF.Dependencies.Helpers;

namespace UTTAF.Desktop.ViewModels
{
    internal class ConfigureViewModel : ViewModelBase
    {
        public ConfigureViewModel() => Init();

        private void Init()
        {
        }

        private void GenerateQrCode()
        {
            using var generator = new QRCodeGenerator();
            using QRCodeData data = generator.CreateQrCode(DataHelper.AuthSession.SessionReference, QRCodeGenerator.ECCLevel.Q);
            using var qRCode = new QRCode(data);
            qRCode.GetGraphic(10, Color.Black, Color.White, false);
        }
    }
}
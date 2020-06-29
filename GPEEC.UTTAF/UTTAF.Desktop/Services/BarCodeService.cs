using QRCoder;

using UTTAF.Desktop.Services.Interfaces;

namespace UTTAF.Desktop.Services
{
	public class BarCodeService : IBarCodeService
	{
		public object GenerateQrCodeTaskAsync(string value)
		{
			using var generator = new QRCodeGenerator();
			using QRCodeData data = generator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
			using var qRCode = new QRCode(data);
			return qRCode.GetGraphic(10, System.Drawing.Color.Black, System.Drawing.Color.White, false);
		}
	}
}
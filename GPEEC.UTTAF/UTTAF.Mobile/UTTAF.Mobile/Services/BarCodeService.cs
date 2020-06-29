using System.Collections.Generic;
using System.Threading.Tasks;

using UTTAF.Mobile.Services.Interfaces;

using ZXing;
using ZXing.Mobile;

namespace UTTAF.Mobile.Services
{
	public class BarCodeService : IBarCodeService
	{
		public async Task<Result> ScanQrCodeTaskAsync()
		{
			return await new MobileBarcodeScanner()
			{
				BottomText = "Aponte para o QrCode",
				TopText = "UTTAF",
				CameraUnsupportedMessage = "Camera não suportada!"
			}.Scan(new MobileBarcodeScanningOptions
			{
				AutoRotate = false,
				PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
			});
		}
	}
}
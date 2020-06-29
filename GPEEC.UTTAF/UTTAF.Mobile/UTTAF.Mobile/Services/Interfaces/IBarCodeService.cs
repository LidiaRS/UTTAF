using System.Threading.Tasks;

using ZXing;

namespace UTTAF.Mobile.Services.Interfaces
{
	public interface IBarCodeService
	{
		Task<Result> ScanQrCodeTaskAsync();
	}
}
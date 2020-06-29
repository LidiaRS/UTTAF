namespace UTTAF.Desktop.Services.Interfaces
{
	public interface IBarCodeService
	{
		object GenerateQrCodeTaskAsync(string value);
	}
}
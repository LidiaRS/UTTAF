namespace UTTAF.Desktop.Services.Interfaces
{
	public interface IBarCodeService
	{
		object GenerateQrCode(string value);
	}
}
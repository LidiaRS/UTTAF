using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace UTTAF.Desktop.Converters
{
	internal class ImageTypeConverter
	{
		internal static BitmapImage BitmapToBitmapImage(Bitmap src)
		{
			using var ms = new MemoryStream();
			src.Save(ms, ImageFormat.Bmp);
			var image = new BitmapImage();
			image.BeginInit();
			ms.Seek(0, SeekOrigin.Begin);
			image.StreamSource = ms;
			image.CacheOption = BitmapCacheOption.OnLoad;
			image.EndInit();
			return image;
		}
	}
}
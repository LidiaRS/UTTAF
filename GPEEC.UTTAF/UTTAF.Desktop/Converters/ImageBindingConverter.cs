using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace UTTAF.Desktop.Converters
{
	public class ImageBindingConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
			value is null ? null : ImageTypeConverter.BitmapToBitmapImage(value as Bitmap);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}
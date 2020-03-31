using System;
using System.Globalization;

using Xamarin.Forms;

namespace UTTAF.Mobile.Converters.Binding
{
    internal class JoinedMessageBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            $"Olá {(string)value}, A sessao irá começar em breve.";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
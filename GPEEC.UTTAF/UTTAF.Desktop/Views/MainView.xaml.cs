using System.Windows;

using UTTAF.Dependencies.Helpers;
using UTTAF.Desktop.Services.Requests;

namespace UTTAF.Desktop.Views
{
    public partial class MainView : Window
    {
        public MainView() => InitializeComponent();

        public MainView(Window window) : this() => window.Close();

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await SessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);
        }
    }
}
using System.Windows;

using UTTAF.Dependencies.Helpers;
using UTTAF.Desktop.Services.Requests;

namespace UTTAF.Desktop.Views
{
	public partial class MainView : Window
	{
		private readonly SessionRequestService _sessionService;

		public MainView(SessionRequestService sessionService)
		{
			_sessionService = sessionService;

			InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			await _sessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);
			Close();
		}
	}
}
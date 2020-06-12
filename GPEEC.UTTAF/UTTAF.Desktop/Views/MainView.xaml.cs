using System.Windows;

using UTTAF.Dependencies.Helpers;
using UTTAF.Desktop.Services.Requests;

namespace UTTAF.Desktop.Views
{
	public partial class MainView : Window
	{
		private readonly SessionService _sessionService;
		private readonly ConfigureView _configureView;

		public MainView(SessionService sessionService, ConfigureView configureView)
		{
			_sessionService = sessionService;
			_configureView = configureView;

			InitializeComponent();

			_configureView.Close();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			await _sessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);
			Application.Current.MainWindow = _configureView;
			Close();
			_configureView.Show();
		}
	}
}
using System.Windows;

using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
	public partial class MainView : Window
	{
		public MainView(MainViewModel mainViewModel)
		{
			InitializeComponent();
			DataContext = mainViewModel;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//await _sessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);
			Close();
		}
	}
}
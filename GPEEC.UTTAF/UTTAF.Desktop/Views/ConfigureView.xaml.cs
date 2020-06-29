using System.Windows;

using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
	public partial class ConfigureView : Window
	{
		public ConfigureView(ConfigureViewModel configureViewModel, SessionConnection sessionConnection)
		{
			InitializeComponent();
			DataContext = configureViewModel;

			Closing += async (s, e) => await sessionConnection.Connection.DisposeAsync();
		}

		public void CancelSession()
		{
			//if (Application.Current.MainWindow is ConfigureView configure)
			//{
			//    var view = ((((configure.Content as Grid).Children[2] as Grid).Children[0] as Transitioner).Items[0] as TransitionerSlide).Content as StartView;
			//    view.StartCreateSession.Visibility = Visibility.Visible;
			//    view.NextCreateSession.Visibility = Visibility.Collapsed;

			//    Transitioner.MoveFirstCommand.Execute(null, null);
			//}
		}
	}
}
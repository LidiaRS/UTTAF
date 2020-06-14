using System.Windows;

using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
	public partial class ConfigureView : Window
	{
		public ConfigureView(ConfigureViewModel configureViewModel)
		{
			InitializeComponent();

			DataContext = configureViewModel;
		}

		private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//var transitioner = ((Content as Grid).Children[2] as Grid).Children[0] as Transitioner;
			//var createSessionView = (transitioner.Items[1] as TransitionerSlide).Content as CreateSessionView;
			//if (transitioner.SelectedIndex == 1 || ((transitioner.Items[0] as TransitionerSlide).Content as StartView).NextCreateSession.Visibility == Visibility.Visible)
			//    (createSessionView.DataContext as CreateSessionViewModel).CancelSessionCreationCommand.Execute(createSessionView);
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

		private void CreateSession(object sender, RoutedEventArgs e) => (DataContext as ConfigureViewModel).InitSession();
	}
}
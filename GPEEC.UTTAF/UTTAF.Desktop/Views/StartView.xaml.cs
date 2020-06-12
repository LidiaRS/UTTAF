using MaterialDesignThemes.Wpf.Transitions;

using System.Windows;
using System.Windows.Controls;

using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
	public partial class StartView : UserControl
	{
		public StartView(StartViewModel startViewModel)
		{
			InitializeComponent();

			DataContext = startViewModel;
		}

		private void CreateSession(object sender, RoutedEventArgs e) =>
			(((((((Application.Current.MainWindow as ConfigureView).Content as Grid).Children[2] as Grid).Children[0] as Transitioner).Items[1] as TransitionerSlide).Content as CreateSessionView).DataContext as CreateSessionViewModel).Init();
	}
}
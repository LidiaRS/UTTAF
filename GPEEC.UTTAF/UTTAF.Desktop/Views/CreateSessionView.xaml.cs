using MaterialDesignThemes.Wpf.Transitions;

using System.Windows;
using System.Windows.Controls;

using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
    public partial class CreateSessionView : UserControl
    {
        public CreateSessionView()
        {
            InitializeComponent();
            DataContext = new CreateSessionViewModel();
        }

        private void FinishSession(object sender, RoutedEventArgs e)
        {
            new MainView(Application.Current.MainWindow).Show();
        }

        internal void CancelSession()
        {
            if (Application.Current.MainWindow is ConfigureView configure)
            {
                var view = ((((configure.Content as Grid).Children[2] as Grid).Children[0] as Transitioner).Items[0] as TransitionerSlide).Content as StartView;
                view.StartCreateSession.Visibility = Visibility.Visible;
                view.NextCreateSession.Visibility = Visibility.Collapsed;

                Transitioner.MoveFirstCommand.Execute(null, null);
            }
        }
    }
}
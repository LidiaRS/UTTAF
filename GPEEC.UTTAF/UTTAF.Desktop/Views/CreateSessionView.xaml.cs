using MaterialDesignThemes.Wpf.Transitions;

using System.Windows;
using System.Windows.Controls;

namespace UTTAF.Desktop.Views
{
    /// <summary>
    /// Interação lógica para CreateSessionView.xam
    /// </summary>
    public partial class CreateSessionView : UserControl
    {
        public CreateSessionView()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow as ConfigureView).DataContext;
        }

        private void FinishSession(object sender, RoutedEventArgs e)
        {
            new MainView(Application.Current.MainWindow).Show();
        }

        private void CancelSession(object sender, RoutedEventArgs e)
        {
            var view = (((((Application.Current.MainWindow as ConfigureView).Content as Grid).Children[2] as Grid).Children[0] as Transitioner).Items[0] as TransitionerSlide).Content as StartView;
            view.StartCreateSession.Visibility = Visibility.Visible;
            view.NextCreateSession.Visibility = Visibility.Collapsed;
        }
    }
}
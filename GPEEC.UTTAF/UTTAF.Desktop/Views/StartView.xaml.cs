using System.Windows;
using System.Windows.Controls;

namespace UTTAF.Desktop.Views
{
    /// <summary>
    /// Interação lógica para StartView.xam
    /// </summary>
    public partial class StartView : UserControl
    {
        public StartView() => InitializeComponent();

        private void CreateSession(object sender, RoutedEventArgs e)
        {
            MaterialDesignThemes.Wpf.DialogHost.Show(new DialogHost.InputNewSessionNameView(this), "CreateSessionDH", (s, e) =>
            {
                if ((bool)e.Parameter == true)
                {
                    StartCreateSession.Visibility = Visibility.Collapsed;
                    NextCreateSession.Visibility = Visibility.Visible;
                }
            });
        }
    }
}
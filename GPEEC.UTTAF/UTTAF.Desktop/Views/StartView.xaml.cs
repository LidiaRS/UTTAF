using MaterialDesignThemes.Wpf;

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
            MaterialDesignThemes.Wpf.DialogHost.Show(new DialogHost.InputNewSessionNameView(), "CreateSessionDH", CreateSessionClose);
        }

        private void CreateSessionClose(object sender, DialogClosingEventArgs e)
        {
            if ((bool)e.Parameter == true)
            {
                ButtonAux.Command.Execute(null);
            }
        }
    }
}
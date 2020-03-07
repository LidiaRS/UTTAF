using System.Windows;

namespace UTTAF.Desktop.Views
{
    /// <summary>
    /// Lógica interna para StartView.xaml
    /// </summary>
    public partial class StartView : Window
    {
        public StartView() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e) => new MainView(this).Show();
    }
}
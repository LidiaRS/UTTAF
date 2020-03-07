using System.Windows;

namespace UTTAF.Desktop.Views
{
    /// <summary>
    /// Lógica interna para MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView() => InitializeComponent();

        public MainView(Window window) : this() => window.Close();

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
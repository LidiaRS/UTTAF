using System.Windows;

namespace UTTAF.Desktop.Views
{
    public partial class MainView : Window
    {
        public MainView() => InitializeComponent();

        public MainView(Window window) : this() => window.Close();
    }
}
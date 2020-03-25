using System.Windows;

using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
    /// <summary>
    /// Lógica interna para StartView.xaml
    /// </summary>
    public partial class ConfigureView : Window
    {
        public ConfigureView()
        {
            InitializeComponent();
            DataContext = new ConfigureViewModel();
        }
    }
}
using System.Windows.Controls;

using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
            DataContext = new StartViewModel();
        }
    }
}
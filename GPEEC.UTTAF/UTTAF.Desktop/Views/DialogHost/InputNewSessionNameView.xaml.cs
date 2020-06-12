using System.Windows;
using System.Windows.Controls;

namespace UTTAF.Desktop.Views.DialogHost
{
    /// <summary>
    /// Interação lógica para InputNewSessionNameView.xam
    /// </summary>
    public partial class InputNewSessionNameView : UserControl
    {
        private readonly ConfigureView _view;

        public InputNewSessionNameView(ConfigureView view)
        {
            InitializeComponent();
            _view = view;
        }

        private void CreateSession(object sender, RoutedEventArgs e)
        {
            _view.NextCreateSession.Command.Execute(null);
        }
    }
}
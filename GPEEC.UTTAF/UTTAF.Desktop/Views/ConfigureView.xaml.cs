using MaterialDesignThemes.Wpf.Transitions;

using System.Windows;
using System.Windows.Controls;

using UTTAF.Desktop.ViewModels;

namespace UTTAF.Desktop.Views
{
    public partial class ConfigureView : Window
    {
        public ConfigureView() => InitializeComponent();

        private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var transitioner = ((Content as Grid).Children[2] as Grid).Children[0] as Transitioner;
            var createSessionView = (transitioner.Items[1] as TransitionerSlide).Content as CreateSessionView;
            if (transitioner.SelectedIndex == 1 || ((transitioner.Items[0] as TransitionerSlide).Content as StartView).NextCreateSession.Visibility == Visibility.Visible)
                (createSessionView.DataContext as CreateSessionViewModel).CancelSessionCreationCommand.Execute(createSessionView);
        }
    }
}
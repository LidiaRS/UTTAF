using UTTAF.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UTTAF.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JoinedSessionView : ContentPage
    {
        public JoinedSessionView()
        {
            InitializeComponent();
            BindingContext = new JoinedSessionViewModel();
        }
    }
}
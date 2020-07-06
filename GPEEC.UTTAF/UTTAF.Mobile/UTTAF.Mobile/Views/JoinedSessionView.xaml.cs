using UTTAF.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UTTAF.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JoinedSessionView : ContentPage
	{
		public JoinedSessionView(JoinedSessionViewModel joinedSessionViewModel)
		{
			InitializeComponent();
			BindingContext = joinedSessionViewModel;
		}

		protected override bool OnBackButtonPressed() => true;
	}
}
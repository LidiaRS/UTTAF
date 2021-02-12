using Xamarin.Forms;

namespace UTTAF.Mobile.Home
{
	public partial class MainView : ContentPage
	{
		public MainView()
		{
			InitializeComponent();
			BindingContext = new MainViewModel();
		}
	}
}
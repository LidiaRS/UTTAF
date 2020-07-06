using Logikoz.XamarinUtilities.Utilities;

using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UTTAF.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartView : ContentPage
	{
		public StartView() => InitializeComponent();

		private async void JoinSession(object sender, System.EventArgs e)
		{
			await PopPushViewUtil.PushModalAsync(((App)Application.Current).ServiceProvider.GetRequiredService<JoinSessionView>());
		}
	}
}
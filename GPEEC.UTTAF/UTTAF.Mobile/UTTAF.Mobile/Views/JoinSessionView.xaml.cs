using System;

using UTTAF.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UTTAF.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JoinSessionView : ContentPage
	{
		public JoinSessionView(JoinSessionViewModel joinSessionViewModel)
		{
			InitializeComponent();

			BindingContext = joinSessionViewModel;
		}

		private async void BackToStart(object sender, EventArgs e)
		{
			await Application.Current.MainPage.Navigation.PopModalAsync();
		}
	}
}
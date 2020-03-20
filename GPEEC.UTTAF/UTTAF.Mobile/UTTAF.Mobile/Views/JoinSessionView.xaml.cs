using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UTTAF.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JoinSessionView : ContentPage
    {
        public JoinSessionView() => InitializeComponent();

        private async void BackToStart(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}
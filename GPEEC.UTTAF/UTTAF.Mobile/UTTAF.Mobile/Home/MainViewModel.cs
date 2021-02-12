using System;
using System.Threading.Tasks;
using System.Windows.Input;

using UTTAF.Dependencies.Clients.Utils;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace UTTAF.Mobile.Home
{
	public class MainViewModel : PropertyNotifier
	{
		public ICommand GoToWebSiteCommand { get; set; }

		public MainViewModel()
		{
			GoToWebSiteCommand = new Command<string>(async x => await GoToWebSiteAsync(x));
		}

		private static async Task GoToWebSiteAsync(string uri)
		{
			await Launcher.OpenAsync(new Uri(uri));
		}
	}
}
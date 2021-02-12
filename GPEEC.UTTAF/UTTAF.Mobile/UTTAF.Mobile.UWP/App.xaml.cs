using System;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UTTAF.Mobile.UWP
{
	sealed partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Suspending += OnSuspending;

			UnhandledException += async (sender, e) =>
			{
				e.Handled = true;

				await new MessageDialog($"Erro: {e.Message}", "Algo de errado aconteceu.").ShowAsync();
			};
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				DebugSettings.EnableFrameRateCounter = true;
			}

#endif

			if (!(Window.Current.Content is Frame rootFrame))
			{
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				Xamarin.Forms.Forms.Init(e);

				Window.Current.Content = rootFrame;
			}

			if (rootFrame.Content == null)
				rootFrame.Navigate(typeof(MainPage), e.Arguments);

			Window.Current.Activate();
		}

		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e) => throw new Exception("Failed to load Page " + e.SourcePageType.FullName);

		private void OnSuspending(object sender, SuspendingEventArgs e) => e.SuspendingOperation.GetDeferral().Complete();
	}
}
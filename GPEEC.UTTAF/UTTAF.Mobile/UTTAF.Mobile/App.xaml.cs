﻿using UTTAF.Mobile.Home;

using Xamarin.Forms;

namespace UTTAF.Mobile
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new MainView();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
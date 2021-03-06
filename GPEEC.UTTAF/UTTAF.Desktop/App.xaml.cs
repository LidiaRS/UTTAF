﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Windows;

using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Desktop.Services;
using UTTAF.Desktop.Services.Interfaces;
using UTTAF.Desktop.ViewModels;
using UTTAF.Desktop.Views;

namespace UTTAF.Desktop
{
	public partial class App : Application
	{
		public IServiceProvider ServiceProvider { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{

			DispatcherUnhandledException += (object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) =>
			{
				MessageBox.Show(e.Exception.Message);
			};

			ServiceProvider = Host.CreateDefaultBuilder().ConfigureServices(ConfigureServices).Build().Services;

			base.OnStartup(e);
		}

		private void ConfigureServices(IServiceCollection services)
		{
			//Views
			services.AddSingleton<ConfigureView>();
			services.AddSingleton<MainView>();

			//ViewModels
			services.AddTransient<ConfigureViewModel>();
			services.AddTransient<MainViewModel>();

			// --------- Services ---------
			services.AddSingleton<SessionHubService>();
			services.AddSingleton<IBarCodeService, BarCodeService>();

			//Hub Connections
			services.AddSingleton(new SessionConnection(DataHelper.URL));

			// --------- Services ---------
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			ServiceProvider.GetRequiredService<ConfigureView>().Show();
			ServiceProvider.GetRequiredService<SessionConnection>();
		}
	}
}
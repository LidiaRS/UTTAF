using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Windows;

using UTTAF.Dependencies.Clients.Services;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Desktop.ViewModels;
using UTTAF.Desktop.Views;

namespace UTTAF.Desktop
{
	public partial class App : Application
	{
		public IServiceProvider ServiceProvider { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			ServiceProvider = new HostBuilder().ConfigureServices(ConfigureServices).Build().Services;

			base.OnStartup(e);
		}

		private void ConfigureServices(IServiceCollection services)
		{
			//Views
			services.AddScoped<ConfigureView>();
			services.AddScoped<MainView>();

			//ViewModels
			services.AddScoped<ConfigureViewModel>();
			services.AddScoped<MainViewModel>();

			// --------- Services ---------
			services.AddSingleton<SessionService>();

			//Hub Connections
			services.AddSingleton<SessionConnection>();

			// --------- Services ---------
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			ServiceProvider.GetRequiredService<ConfigureView>().Show();
		}
	}
}
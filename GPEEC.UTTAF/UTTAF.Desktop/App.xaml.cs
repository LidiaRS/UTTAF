using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Windows;

using UTTAF.Desktop.Services.Requests;
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

			//Services
			services.AddSingleton<SessionService>();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			ServiceProvider.GetService<ConfigureView>().Show();
		}
	}
}
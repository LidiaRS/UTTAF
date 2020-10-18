using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Threading.Tasks;
using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Clients.Services.HubConnections;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Services.Interfaces;
using UTTAF.Mobile.ViewModels;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile
{
	public partial class App : Application
	{
		public IServiceProvider ServiceProvider { get; private set; }

		public App()
		{
			InitializeComponent();

			var hostBuilder = new HostBuilder();
			hostBuilder.UseContentRoot(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
			ServiceProvider = hostBuilder.ConfigureServices(ConfigureServices).Build().Services;
		}

		private void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
		{
			//Views
			services.AddScoped<StartView>();
			services.AddScoped<JoinSessionView>();
			services.AddScoped<MovingRobotView>();
			services.AddScoped<JoinedSessionView>();

			//ViewModels
			services.AddScoped<JoinSessionViewModel>();
			services.AddScoped<JoinedSessionViewModel>();

			//Serices
			services.AddSingleton<IBarCodeService, BarCodeService>();
			services.AddSingleton<AttendeeHubService>();

			services.AddSingleton(new SessionConnection(DataHelper.URLMobile));
		}

		protected override void OnStart()
		{
			MainPage = ServiceProvider.GetRequiredService<StartView>();

			ServiceProvider.GetRequiredService<SessionConnection>();
		}

		protected async override void OnSleep()
		{
			await ServiceProvider.GetRequiredService<SessionConnection>().Connection.StopAsync();
		}

		protected override void OnResume()
		{
		}
	}
}
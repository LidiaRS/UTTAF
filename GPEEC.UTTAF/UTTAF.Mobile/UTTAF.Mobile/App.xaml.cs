using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

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

			ServiceProvider = Host.CreateDefaultBuilder().ConfigureServices(ConfigureServices).Build().Services;
		}

		private void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
		{
			//Views
			services.AddScoped<StartView>();
			services.AddScoped<JoinSessionView>();
			services.AddSingleton<MovingRobotView>();
			services.AddScoped<JoinedSessionView>();

			//ViewModels
			services.AddScoped<JoinSessionViewModel>();
			services.AddScoped<JoinedSessionViewModel>();

			//Serices
			services.AddSingleton<IBarCodeService, BarCodeService>();
			services.AddSingleton<AttendeeHubService>();
		}

		protected override void OnStart()
		{
			MainPage = ServiceProvider.GetRequiredService<StartView>();
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
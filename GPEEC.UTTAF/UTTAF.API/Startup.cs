using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Text.Json;

using UTTAF.API.Business;
using UTTAF.API.Business.Interfaces;
using UTTAF.API.Data;
using UTTAF.API.Data.Converters;
using UTTAF.API.Hubs;
using UTTAF.API.Repository;
using UTTAF.API.Repository.Interfaces;

namespace UTTAF.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSignalR();

			services.AddDbContext<DataContext>
			(
				options => options.UseSqlite(Configuration["ConnectionString"],
				builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
			);

			//Businesses
			services.AddScoped<ISessionBusiness, SessionBusiness>();
			services.AddScoped<IAttendeeBusiness, AttendeeBusiness>();

			//Repositories
			services.AddScoped<ISessionRepository, SessionRepository>();
			services.AddScoped<IAttendeeRepository, AttendeeRepository>();
			services.AddScoped<IRobotRepository, RobotRepository>();

			//Converters
			services.AddSingleton<AttendeeConverter>();
			services.AddSingleton<SessionConverter>();
			services.AddSingleton<RobotConverter>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				DataContext context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
				context.Database.Migrate();
			}

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async (e) =>
				{
					string[] fullName = typeof(Startup).Assembly.FullName.Split(',');

					await e.Response.WriteAsync(JsonSerializer.Serialize(new
					{
						Status = true,
						Environment = env.EnvironmentName,
						Name = fullName[0],
						Version = fullName[1].Remove(0, 9)
					}));
				});
				endpoints.MapHub<SessionHub>("/session");
				endpoints.MapHub<RobotHub>("/robot");
				endpoints.MapHub<PlatformHub>("/platform");
			});
		}
	}
}
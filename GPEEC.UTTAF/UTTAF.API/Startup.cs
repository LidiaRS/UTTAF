using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using UTTAF.API.Data;
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
			services.AddControllers();
			services.AddSignalR();

			services.AddDbContext<DataContext>
			(
				options => options.UseSqlite(Configuration["ConnectionString"],
				builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
			);

			services.AddScoped<ISessionRepository, SessionRepository>();
			services.AddScoped<IAttendeeRepository, AttendeeRepository>();
			services.AddScoped<IRobotRepository, RobotRepository>();
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

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<SessionHub>("/session");
				endpoints.MapHub<AttendeeHub>("/attendee");
				endpoints.MapHub<RobotHub>("/robot");
				endpoints.MapHub<PlatformHub>("/platform");
			});
		}
	}
}
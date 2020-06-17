using Microsoft.EntityFrameworkCore;

using UTTAF.API.Models;

namespace UTTAF.API.Data
{
	public class DataContext : DbContext
	{
		public DbSet<SessionModel> Sessions { get; set; }
		public DbSet<AttendeeModel> Attendees { get; set; }

		public DbSet<RobotModel> Robots { get; set; }

		public DataContext()
		{
		}

		public DataContext(DbContextOptions options) : base(options)
		{
		}
	}
}
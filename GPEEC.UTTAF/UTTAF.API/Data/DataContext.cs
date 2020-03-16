using Microsoft.EntityFrameworkCore;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<SessionModel> Sessions { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
using Microsoft.EntityFrameworkCore;

using UTTAF.API.Models;

namespace UTTAF.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AuthSessionModel> Sessions { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
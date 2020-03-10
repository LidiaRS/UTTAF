using Microsoft.EntityFrameworkCore;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AuthModel> Auths { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
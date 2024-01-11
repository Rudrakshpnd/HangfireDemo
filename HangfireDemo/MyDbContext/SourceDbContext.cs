using HangfireDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace HangfireDemo.MyDbContext
{
    public class SourceDbContext:DbContext
    {
        public SourceDbContext(DbContextOptions<SourceDbContext> options):base(options)
        {
            
        }

        public DbSet<SourceDbContextModel> SourceTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.;Database=TheTutionWalla;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            // Configure the connection string for the source database
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

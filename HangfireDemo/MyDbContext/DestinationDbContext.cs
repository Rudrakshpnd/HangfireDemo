using HangfireDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace HangfireDemo.MyDbContext
{
    public class DestinationDbContext :DbContext
    {
        public DestinationDbContext(DbContextOptions<DestinationDbContext> options):base(options) 
        {
            
        }
        public DbSet<DestinationDbContextModel> DestinationTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = "Server=.;Database=Student;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            // Configure the connection string for the source database
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

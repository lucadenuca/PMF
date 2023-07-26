using Microsoft.EntityFrameworkCore;
using PMF.Database.Configuration;
using PMF.Database.Entities;
namespace PMF.Database
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TransactionEntity> Transactions { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public ApplicationDbContext() { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            modelBuilder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }



    }
}

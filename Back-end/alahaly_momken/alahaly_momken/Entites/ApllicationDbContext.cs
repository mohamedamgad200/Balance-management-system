using alahaly_momken.Entites;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Your model configuration
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\ProjectModels;Initial Catalog=finalf;Integrated Security=True",
                    options => options.EnableRetryOnFailure());
            }
        }
        public DbSet<Depoist> Depoists { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Bank> banks { get; set; }
        public DbSet<SuperAdmin> superAdmins { get; set; }
        public DbSet<Depit> debts { get; set; }
        public DbSet <Correction>corrections { get; set; }




    }


}

using Microsoft.EntityFrameworkCore;

namespace rimCars_Api.Entities
{
    public class SalonsDbContext : DbContext
    {
        private readonly string _connectionString=
            "Server=DESKTOP-P6EHEFN\\SQLEXPRESS;DataBase=SalonDb;Trusted_Connection=True;";
        public DbSet<Salon> Salons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Rim> Rims { get; set; }

        protected override void OnModelCreating (ModelBuilder model)
        {
            model.Entity<Salon>()
                .Property(r => r.nrTel)
                .IsRequired();

            model.Entity<Address>()
                .Property(a => a.City)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}

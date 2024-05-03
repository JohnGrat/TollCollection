using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TollCollectionDbContext : DbContext
    {
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<TollPassage> TollPassages { get; set; }

        public TollCollectionDbContext(DbContextOptions<TollCollectionDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleType>()
                .HasIndex(vt => vt.TypeName)
                .IsUnique();

            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType { Id = 1, TypeName = "Motorbike" },
                new VehicleType { Id = 2, TypeName = "Tractor" },
                new VehicleType { Id = 3, TypeName = "Emergency" },
                new VehicleType { Id = 4, TypeName = "Diplomat" },
                new VehicleType { Id = 5, TypeName = "Foreign" },
                new VehicleType { Id = 6, TypeName = "Military" },
                new VehicleType { Id = 7, TypeName = "Taxable" }
            );
        }
    }
}

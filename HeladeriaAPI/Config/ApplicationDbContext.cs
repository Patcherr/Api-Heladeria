using HeladeriaAPI.Models.Estado;
using HeladeriaAPI.Models.Helado;
using HeladeriaAPI.Models.Ingrediente;
using HeladeriaAPI.Models.Salsa;
using Microsoft.EntityFrameworkCore;

namespace HeladeriaAPI.Config
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Helado> Helados { get; set; } = null!;
        public DbSet<Estado> Estados { get; set; } = null!;
        public DbSet<Ingrediente> Ingredientes { get; set; } = null!;
        public DbSet<Salsa> Salsas { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>().HasIndex(e => e.Nombre).IsUnique();
            modelBuilder.Entity<Estado>().HasData(
                new Estado { Id = 1, Nombre = "Disponible" },
                new Estado { Id = 2, Nombre = "Pendiente" },
                new Estado { Id = 3, Nombre = "No Disponible" }
            );
            modelBuilder.Entity<Ingrediente>().HasIndex(e => e.Nombre).IsUnique();
            modelBuilder.Entity<Ingrediente>().HasData(
                new Ingrediente { Id = 1, Nombre = "Leche" },
                new Ingrediente { Id = 2, Nombre = "Azucar" },
                new Ingrediente { Id = 3, Nombre = "Alcohol" },
                new Ingrediente { Id = 4, Nombre = "Chocolate" },
                new Ingrediente { Id = 5, Nombre = "Crema" },
                new Ingrediente { Id = 6, Nombre = "Agua" },
                new Ingrediente { Id = 7, Nombre = "Frutilla"}
            );
            // Para que la fecha de creación se establezca automáticamente al crear un nuevo helado
            modelBuilder.Entity<Helado>().Property(h => h.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");            
            
            modelBuilder.Entity<Helado>()
            .HasMany(u => u.Ingredientes)
            .WithMany()
            .UsingEntity<IngredienteHelado>(
                l => l.HasOne<Ingrediente>().WithMany().HasForeignKey(e => e.IngredienteId),
                r => r.HasOne<Helado>().WithMany().HasForeignKey(e => e.HeladoId)
            );

            modelBuilder.Entity<Salsa>()
            .HasMany(u => u.Ingredientes)
            .WithMany()
            .UsingEntity<IngredienteSalsa>(
                l => l.HasOne<Ingrediente>().WithMany().HasForeignKey(e => e.IngredienteId),
                r => r.HasOne<Salsa>().WithMany().HasForeignKey(e => e.SalsaId)
            );

        }
    }
}

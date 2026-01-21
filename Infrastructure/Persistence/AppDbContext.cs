using GetechnologiesMx.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GetechnologiesMx.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PersonaEntity> Personas => Set<PersonaEntity>();
    public DbSet<FacturaEntity> Facturas => Set<FacturaEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonaEntity>(entity =>
        {
            entity.ToTable("Personas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.Identificacion).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Identificacion).IsUnique();
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relación: Una Persona tiene muchas Facturas
            entity.HasMany(p => p.Facturas)
                  .WithOne(f => f.Persona)
                  .HasForeignKey(f => f.PersonaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FacturaEntity>(entity =>
        {
            entity.ToTable("Facturas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NumeroFactura).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Fecha).IsRequired();
            entity.Property(e => e.PersonaId).IsRequired();
        });
    }
}

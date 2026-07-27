using Chat.Servidor.Entidades;
using Microsoft.EntityFrameworkCore;
namespace Chat.Servidor.Data;

public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
{
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Mensaje> Mensajes => Set<Mensaje>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            entity.Property(s => s.Nombre)
                .IsRequired();
            entity.HasIndex(s => s.Nombre)
                .IsUnique();

            entity.HasOne(s => s.Anfitrion)
                .WithOne()
                .HasForeignKey<Sala>(s => s.AnfitrionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(s => s.Usuarios)
               .WithOne(u => u.Sala)
               .HasForeignKey(u => u.SalaId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(s => s.Mensajes)
                .WithOne(m => m.Sala)
                .HasForeignKey(m => m.SalaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            entity.Property(u => u.Nombre)
                .IsRequired();
            entity.HasIndex(u => u.Nombre)
                .IsUnique();

            entity.HasMany(u => u.Mensajes)
                .WithOne(m => m.Usuario)
                .HasForeignKey(m => m.UsuarioId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            entity.Property(m => m.Texto)
                .IsRequired();
        });
    }
}

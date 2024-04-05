using CreaMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreaMT.infrastructure.DataAcess;
public class CreaMTAPIDbContext : DbContext
{
    public CreaMTAPIDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreaMTAPIDbContext).Assembly);
    }
}

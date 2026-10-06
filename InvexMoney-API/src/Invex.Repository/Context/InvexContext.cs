
using Invex.Repository.Configurations;
using Microsoft.EntityFrameworkCore;
using Invex.Domain.Entidades;

public class InvexContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }


    public InvexContext(DbContextOptions options ) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
    }
}
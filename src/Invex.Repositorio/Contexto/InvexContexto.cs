using Microsoft.EntityFrameworkCore;
using Invex.Dominio.Entidades;
using Invex.Repositorio.Mapeamentos;

public class InvexContexto : DbContext
{
    private readonly DbContextOptions _options;

    public DbSet<Ativo> Ativos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Dividendo> Dividendos { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }


    public InvexContexto(DbContextOptions options) : base(options){}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AtivoConfig());
        modelBuilder.ApplyConfiguration(new UsuarioConfig());
        modelBuilder.ApplyConfiguration(new TransacaoConfig());
        modelBuilder.ApplyConfiguration(new DividendoConfig());
    }
}
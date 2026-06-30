using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invex.Dominio.Entidades;

namespace Invex.Repositorio.Mapeamentos;

public class AtivoConfig : IEntityTypeConfiguration<Ativo>
{
    public void Configure(EntityTypeBuilder<Ativo> builder)
    {
        builder.ToTable("Ativos").HasKey(a => a.Id);

        builder.Property(nameof(Ativo.Id)).HasColumnName("AtivoId");
        builder.Property(nameof(Ativo.Nome)).HasColumnName("Nome")
        .IsRequired(true)
        .HasMaxLength(100);

        builder.Property(nameof(Ativo.Ticker)).HasColumnName("Ticker")
        .IsRequired(true);

        builder.Property(nameof(Ativo.Tipo)).HasColumnName("Tipo")
        .IsRequired(true);

        builder.Property(nameof(Ativo.DataCriacao)).HasColumnName("DataCriacao")
        .IsRequired(true);



        builder
        .HasOne(ativo => ativo.Usuario)
        .WithMany(usuario => usuario.Ativos) // 1 usuario tem n ativos
        .HasForeignKey(ativo => ativo.UsuarioId);

        builder
        .HasMany(ativo => ativo.Transacoes) // 1 Ativo tem n Transacoes
        .WithOne(transacao => transacao.Ativo) // Cada Transação pertence a um Ativo
        .HasForeignKey(transacao => transacao.AtivoId);


        builder
        .HasMany(ativo => ativo.Dividendos) // 1 Ativo tem n Dividendos
        .WithOne(dividendo => dividendo.Ativo) // cada Dividendo pertence a um Ativo
        .HasForeignKey(dividendo => dividendo.AtivoId);



    }
}
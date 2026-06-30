using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invex.Dominio.Entidades;


namespace Invex.Repositorio.Mapeamentos;

public class TransacaoConfig : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes").HasKey(t => t.Id);

        builder.Property(nameof(Transacao.Id)).HasColumnName("Id");

        builder.Property(nameof(Transacao.AtivoId)).HasColumnName("AtivoId").IsRequired();

        builder.Property(nameof(Transacao.Tipo)).HasColumnName("Tipo").IsRequired();
        builder.Property(nameof(Transacao.PrecoUnitario)).HasColumnName("PrecoUnitario").IsRequired().HasPrecision(18, 2);

        builder.Property(nameof(Transacao.Quantidade)).HasColumnName("Quantidade").IsRequired().HasPrecision(18, 2);
        builder.Property(nameof(Transacao.Data)).HasColumnName("Data").IsRequired();

        builder.Property(nameof(Transacao.Observacao)).HasColumnName("Observacao").HasMaxLength(500);


    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invex.Dominio.Entidades;

namespace Invex.Repositorio.Mapeamentos;

public class DividendoConfig : IEntityTypeConfiguration<Dividendo>
{
    public void Configure(EntityTypeBuilder<Dividendo> builder)
    {
        builder.ToTable("Dividendos").HasKey(d => d.Id);

        builder.Property(nameof(Dividendo.Id)).HasColumnName("Id");
        builder.Property(nameof(Dividendo.ValorPorCota)).HasColumnName("ValorPorCota")
        .HasPrecision(18, 2);
        builder.Property(nameof(Dividendo.QuantidadeNaData)).HasColumnName("QuantidadeNaData").IsRequired().HasPrecision(18, 2);

        builder.Property(nameof(Dividendo.DataPagamento)).HasColumnName("DataPagamento").IsRequired();


    }
}
using Microsoft.EntityFrameworkCore;
using Invex.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Invex.Repository.Configurations;

public class RelatorioConfigurations : IEntityTypeConfiguration<Relatorio>
{
    public void Configure(EntityTypeBuilder<Relatorio> builder)
    {
        builder.ToTable("Relatorio").HasKey(r => r.Id);
        builder.Property(nameof(Relatorio.Id)).HasColumnName("Id");
        builder.Property(nameof(Relatorio.DataReferencia)).HasColumnName("DataReferencia").IsRequired(true);
        builder.Property(nameof(Relatorio.GeradoEm)).HasColumnName("GeradoEm").IsRequired(true);
        builder.Property(nameof(Relatorio.Conteudo)).HasColumnName("Conteudo").IsRequired(true).HasColumnType("NVARCHAR(MAX)");
    }
}
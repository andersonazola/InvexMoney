using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invex.Domain.Entidades;

namespace Invex.Repository.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios").HasKey(u => u.Id);
        builder.Property(nameof(Usuario.Id)).HasColumnName("UsuarioId");
        builder.Property(nameof(Usuario.Nome)).HasColumnName("Nome").IsRequired(true).HasMaxLength(100);
        builder.Property(nameof(Usuario.Email)).HasColumnName("Email").IsRequired(true).HasMaxLength(70);
        builder.Property(nameof(Usuario.Senha)).HasColumnName("Senha").IsRequired(true).HasMaxLength(30);
        builder.Property(nameof(Usuario.DataCriacao)).HasColumnName("DataCriacao").IsRequired(true);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Invex.Dominio.Entidades;


namespace Invex.Repositorio.Mapeamentos;

public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios").HasKey(usuario => usuario.Id);
        
        builder.Property(nameof(Usuario.Id)).HasColumnName("Id");

        builder.Property(nameof(Usuario.Nome)).HasColumnName("Nome")
        .IsRequired(true)
        .HasMaxLength(100);


        builder.Property(nameof(Usuario.Email)).HasColumnName("Email")
        .IsRequired(true);


        builder.Property(nameof(Usuario.SenhaHash)).HasColumnName("Senha")
        .IsRequired(true);


        builder.Property(nameof(Usuario.DataCriacao)).HasColumnName("DataCriacao")
        .IsRequired(true);


    }
}
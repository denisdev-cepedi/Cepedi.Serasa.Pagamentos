using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Pagamento.Dados.EntityTypeConfiguration;

public class CredorEntityTypeConfiguration : IEntityTypeConfiguration<CredorEntity>
{
    public void Configure(EntityTypeBuilder<CredorEntity> builder)
    {
        builder.ToTable("Credor");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(150);
    }
}



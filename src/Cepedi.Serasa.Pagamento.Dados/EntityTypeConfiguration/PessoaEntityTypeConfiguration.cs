using Cepedi.Serasa.Pagamento.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Pagamento.Data.EntityTypeConfiguration;
public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<PessoaEntity>
{
    public void Configure(EntityTypeBuilder<PessoaEntity> builder)
    {
        builder.ToTable("Pessoa");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Cpf).IsRequired().HasMaxLength(12);
    }
}

using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Pagamento.Dados.EntityTypeConfiguration;

public class DividaEntityTypeConfiguration : IEntityTypeConfiguration<DividaEntity>
{
    public void Configure(EntityTypeBuilder<DividaEntity> builder)
    {
        builder.ToTable("Divida");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Valor).IsRequired();
        builder.Property(c => c.DataDeVencimento).IsRequired();

        builder.Property(c => c.IdCredor).IsRequired();

        builder.HasOne<CredorEntity>(c => c.Credor)
            .WithMany()
            .HasForeignKey(c => c.IdCredor);

        builder.Property(c => c.IdPessoa).IsRequired();

        builder.HasOne<PessoaEntity>(c => c.Pessoa).
            WithMany().
            HasForeignKey(c => c.IdPessoa);    
    }
}

using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Pagamento.Dados.EntityTypeConfiguration;

public class PagamentoEntityTypeConfiguration : IEntityTypeConfiguration<PagamentoEntity>
{
    public void Configure(EntityTypeBuilder<PagamentoEntity> builder)
    {
        builder.ToTable("Pagamento");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Valor).IsRequired();
        builder.Property(c => c.DataDePagamento).IsRequired();
        builder.Property(c => c.DataDeVencimento).IsRequired();
        builder.Property(c => c.IdCredor).IsRequired();

        builder.HasOne<CredorEntity>(c => c.Credor)
            .WithMany()
            .HasForeignKey(c => c.IdCredor);
    }
}



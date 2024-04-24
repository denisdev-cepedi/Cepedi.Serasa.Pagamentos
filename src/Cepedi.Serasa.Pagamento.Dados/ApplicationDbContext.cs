using System.Diagnostics.CodeAnalysis;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Pagamento.Dados;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{
    public DbSet<UsuarioEntity> Usuario { get; set; } = default!;
    public DbSet<PagamentoEntity> Pagamento { get; set; } = default!;
    public DbSet<CredorEntity> Credor { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

using Cepedi.Serasa.Pagamento.Dominio.Repositorio;

namespace Cepedi.Serasa.Pagamento.Dominio;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

    IRepository<T> Repository<T>()
        where T : class;
}

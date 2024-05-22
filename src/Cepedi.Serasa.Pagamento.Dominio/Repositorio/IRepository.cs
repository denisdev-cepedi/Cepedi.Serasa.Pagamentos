namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio;

public interface IRepository<T>
    where T : class
{
    Task<T> AdicionarAsync(T entidade, CancellationToken cancellationToken);

    T Atualizar(T entidade);
}

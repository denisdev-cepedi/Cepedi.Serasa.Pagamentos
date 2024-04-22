using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio;

public interface ICredorRepository
{
    Task<CredorEntity> CriarCredorAsync(CredorEntity Credor);
    Task<CredorEntity> ObterCredorAsync(CredorEntity id);
    Task<CredorEntity> AtualizarCredorAsync(CredorEntity Credor);
    Task<CredorEntity> ExcluirCredorAsync(CredorEntity Credor);
}

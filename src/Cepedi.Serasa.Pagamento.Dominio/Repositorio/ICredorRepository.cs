using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio;

public interface ICredorRepository
{
    Task<CredorEntity> CriarCredorAsync(CredorEntity Credor);
    Task<CredorEntity> ObterCredorAsync(int id);
    Task<CredorEntity> AtualizarCredorAsync(CredorEntity Credor);
    Task<CredorEntity> DeletarCredorAsync(int id);
}

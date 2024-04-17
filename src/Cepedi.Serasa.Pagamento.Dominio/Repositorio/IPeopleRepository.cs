using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio;

public interface IPessoaRepository
{
    Task<PessoaEntity> CriarPessoaAsync(PessoaEntity pessoa);
}

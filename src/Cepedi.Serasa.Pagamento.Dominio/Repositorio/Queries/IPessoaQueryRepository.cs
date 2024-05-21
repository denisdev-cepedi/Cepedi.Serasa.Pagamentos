
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;


public interface IPessoaQueryRepository
{
    Task<PessoaEntity?> ObterPessoasDapperAsync(int id);

    Task<PessoaEntity?> AtualizarPessoaDapperAsync(PessoaEntity pessoa);

}


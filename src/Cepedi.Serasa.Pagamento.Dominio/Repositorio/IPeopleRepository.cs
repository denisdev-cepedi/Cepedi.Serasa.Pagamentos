using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio;

public interface IPessoaRepository
{
    Task<PessoaEntity> CriarPessoaAsync(PessoaEntity pessoa);
    Task<PessoaEntity> ObterPessoaAsync(int id);
    Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa);
    Task<PessoaEntity> DeletarPessoaAsync(int id);
}

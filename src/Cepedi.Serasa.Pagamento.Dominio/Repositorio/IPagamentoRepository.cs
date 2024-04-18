using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio;

public interface IPagamentoRepository
{
    Task<PagamentoEntity> CriarPagamentoAsync(PagamentoEntity pagamento);
    Task<PagamentoEntity> ObterPagamentoAsync(int id);
    Task<PagamentoEntity> AtualizarPagamentoAsync(PagamentoEntity pagamento);
    Task<CredorEntity> ObterCredorPagamentoAsync(int id);
}

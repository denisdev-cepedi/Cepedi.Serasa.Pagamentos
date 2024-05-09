using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio;

public interface IDividaRepository
{
    Task<DividaEntity> CriarDividaAsync(DividaEntity divida);
    Task<DividaEntity> ObterDividaAsync(int id);

    Task<List<DividaEntity>> GetDividasAsync();
    Task<DividaEntity> AtualizarDividaAsync(DividaEntity divida);
    Task<DividaEntity> DeletarDividaAsync(int id);
    Task<PessoaEntity> ObterPessoaAsync(int idPessoa);

}

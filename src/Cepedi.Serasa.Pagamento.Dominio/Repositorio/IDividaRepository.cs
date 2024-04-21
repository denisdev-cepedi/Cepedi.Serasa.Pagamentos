using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio;

public interface IDividaRepository
{
    Task<DividaEntity> CriarDividaAsync(DividaEntity divida);
    Task<DividaEntity> ObterDividaAsync(int id);
    Task<DividaEntity> AtualizarDividaAsync(DividaEntity divida);
    Task<DividaEntity> DeletarDividaAsync(int id);
    Task<DividaEntity> ObterDividaPorCredorAsync(int idCredor);
    Task<DividaEntity> ObterDividaPorPessoaAsync(int idPessoa);
    Task<CredorEntity> ObterCredorAsync(int idCredor);
    Task<PessoaEntity> ObterPessoaAsync(int idPessoa);

}

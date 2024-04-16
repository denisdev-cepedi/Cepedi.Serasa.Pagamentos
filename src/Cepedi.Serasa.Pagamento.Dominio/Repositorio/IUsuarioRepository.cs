using Cepedi.Serasa.Pagamento.Dominio.Entidades;

namespace Cepedi.Serasa.Pagamento.Dominio.Repositorio;

public interface IUsuarioRepository
{
    Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario);
    Task<UsuarioEntity> ObterUsuarioAsync(int id);

    Task<UsuarioEntity> AtualizarUsuarioAsync(UsuarioEntity usuario);
}


using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Enums;
public class CredorErros
{
    public static readonly ResultadoErro Generico = new()
    {
        Titulo = "Ops ocorreu um erro no nosso sistema",
        Descricao = "No momento, nosso sistema está indisponível. Por Favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static readonly ResultadoErro SemResultados = new()
    {
        Titulo = "Sua busca não obteve resultados",
        Descricao = "Tente buscar novamente",
        Tipo = ETipoErro.Alerta
    };

    public static readonly ResultadoErro CredorInexistente = new()
    {
        Titulo = "Credor não encontrado",
        Descricao = "Verifique os dados informados e tente novamente",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro ErroGravacaoCredor = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação do usuário. Por favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro DadosInvalidos = new()
    {
        Titulo = "Dados inválidos",
        Descricao = "Os dados enviados na requisição são inválidos",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro CredorJaCadastrado = new()
    {
        Titulo = "Credor já cadastrado",
        Descricao = "O Credor informado já está cadastrado",
        Tipo = ETipoErro.Erro
    };
    
    public static ResultadoErro CredorNaoPodeSerAtualizado = new()
    {
        Titulo = "Credor não pode ser atualizado",
        Descricao = "O Credor informado não pode ser atualizado",
        Tipo = ETipoErro.Erro
    };
    
    public static ResultadoErro CredorNaoPodeSerRemovido = new()
    {
        Titulo = "Credor não pode ser removido",
        Descricao = "O Credor informado não pode ser removido",
        Tipo = ETipoErro.Erro
    };
    
    public static ResultadoErro CredorNaoPossuiDadosAtualizados = new()
    {
        Titulo = "Credor não possui dados atualizados",
        Descricao = "O Credor informado não possui dados atualizados",
        Tipo = ETipoErro.Erro
    };
    
    public static ResultadoErro CredorNaoPossuiDadosParaRemover = new()
    {
        Titulo = "Credor não possui dados para remover",
        Descricao = "O Credor informado não possui dados para remover",
        Tipo = ETipoErro.Erro
    };
    
    public static ResultadoErro CredorNaoPossuiDadosParaAtualizar = new()
    {
        Titulo = "Credor não possui dados para atualizar",
        Descricao = "O Credor informado não possui dados para atualizar",
        Tipo = ETipoErro.Erro
    };

    
    public static ResultadoErro CredorRemovido = new()
    {
        Titulo = "Credor removido com sucesso!",
        Descricao = "O Credor informado foi removido",
        Tipo = ETipoErro.Erro
    };
    
}

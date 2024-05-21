using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Enums;
public class PagamentoErros
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

    public static readonly ResultadoErro ValorPagamentoDiferente = new()
    {
        Titulo = "Valor do pagamento diferente do valor da dívida",
        Descricao = "O valor do pagamento deve ser igual ao valor da dívida",
        Tipo = ETipoErro.Alerta
    };

    public static readonly ResultadoErro PagamentoNaoEncontrado = new()
    {
        Titulo = "Pagamento não encontrado",
        Descricao = "Verifique os dados informados e tente novamente",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro ErroGravacaoUsuario = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação do usuário. Por favor tente novamente",
        Tipo = ETipoErro.Erro
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

    public static ResultadoErro ErroAoEfetuarPagamento = new()
    {
        Titulo = "Erro ao efetuar o pagamento",
        Descricao = "Ocorreu um erro ao efetuar o pagamento. Por favor, verifique os dados e tente novamente",
        Tipo = ETipoErro.Erro
    };
}

using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Enums;
public class DividaErros
{
    public static readonly ResultadoErro DividaNaoEncontrada = new()
    {
        Titulo = "Divida não encontrada",
        Descricao = "Verifique os dados informados e tente novamente",
        Tipo = ETipoErro.Alerta
    };

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

    public static ResultadoErro ErroGravacaoDivida = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação da divida. Por favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro DadosInvalidos = new()
    {
        Titulo = "Dados inválidos",
        Descricao = "Os dados enviados na requisição são inválidos",
        Tipo = ETipoErro.Erro
    };
}



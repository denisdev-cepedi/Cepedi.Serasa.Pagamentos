using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Exceptions;
public class RequestInvalidaExcecao : ExcecaoAplicacao
{
    public RequestInvalidaExcecao(IDictionary<string, string[]> erros)
        : base(PagamentoErros.DadosInvalidos) =>
        Erros = erros.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}");

    public IEnumerable<string> Erros { get; }
}

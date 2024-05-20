using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
public class SemResultadosException : ExcecaoAplicacao
{
    public SemResultadosException() :
        base(PagamentoErros.SemResultados)
    {
    }
}

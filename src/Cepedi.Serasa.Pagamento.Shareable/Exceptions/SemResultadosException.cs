using Cepedi.Serasa.Pagamento.Shareable.Enums;

namespace Cepedi.Serasa.Pagamento.Shareable.Exceptions;
public class SemResultadosException : ApplicationException
{
    public SemResultadosException() : 
        base(BancoCentralMensagemErrors.SemResultados)
    {
    }
}

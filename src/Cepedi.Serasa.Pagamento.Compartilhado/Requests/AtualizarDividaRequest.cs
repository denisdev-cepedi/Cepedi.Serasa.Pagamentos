using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado;

public class AtualizarDividaRequest : IRequest<Result<AtualizarDividaResponse>>
{
    public int Id { get; set; }
    public double Valor { get; set; }

    public DateTime DataDeVencimento { get; set; }
}

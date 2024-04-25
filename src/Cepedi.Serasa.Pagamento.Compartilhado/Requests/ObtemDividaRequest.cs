using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado;

public class ObtemDividaRequest : IRequest<Result<ObtemDividaResponse>>
{
    public int Id { get; set; }
}

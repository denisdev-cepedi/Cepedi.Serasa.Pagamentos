using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado;

public class DeletarDividaRequest : IRequest<Result<DeletarDividaResponse>>
{
    public int Id {get; set;}
}

using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado;

public class ObtemDividasRequest : IRequest<Result<ObtemDividasResponse>>
{
    public int Id { get; set; }
}

using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class ObterPagamentoRequest : IRequest<Result<ObterPagamentoResponse>>
{
    public int Id { get; set; }
}

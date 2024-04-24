using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class DeletarPagamentoRequest : IRequest<Result<DeletarPagamentoResponse>>
{
    public int Id { get; set; }
}

using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class DeletarCredorRequest : IRequest<Result<DeletarCredorResponse>>
{
    public int Id { get; set; }

}

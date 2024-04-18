using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class ObterCredorRequest : IRequest<Result<ObterCredorResponse>>
{
    public string Nome { get; set; } = default!;

}
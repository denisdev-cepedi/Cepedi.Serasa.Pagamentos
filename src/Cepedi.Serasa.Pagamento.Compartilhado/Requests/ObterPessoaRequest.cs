using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class ObterPessoaRequest : IRequest<Result<ObterPessoaResponse>>
{
    public int Id { get; set; }
}

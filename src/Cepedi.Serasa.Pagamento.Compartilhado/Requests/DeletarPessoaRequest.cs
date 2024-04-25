using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class DeletarPessoaRequest : IRequest<Result<DeletarPessoaResponse>>
{
    public int Id { get; set; }
}

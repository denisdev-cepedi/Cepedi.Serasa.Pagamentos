using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class AtualizarPessoaRequest : IRequest<Result<AtualizarPessoaResponse>>
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
}

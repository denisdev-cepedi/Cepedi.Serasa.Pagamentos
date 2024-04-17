using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class CriarPessoaRequest : IRequest<Result<CriarPessoaResponse>>
{
    public string Nome { get; set; } = default!;

    public string Cpf { get; set; } = default!;
}

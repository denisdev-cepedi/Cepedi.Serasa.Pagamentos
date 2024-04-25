using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;

public class CriarPessoaRequestHandler : IRequestHandler<CriarPessoaRequest, Result<CriarPessoaResponse>>
{
    private readonly ILogger<CriarPessoaRequestHandler> _logger;
    private readonly IPessoaRepository _pessoaRepository;

    public CriarPessoaRequestHandler(ILogger<CriarPessoaRequestHandler> logger, IPessoaRepository pessoaRepository)
    {
        _logger = logger;
        _pessoaRepository = pessoaRepository;
    }
    public async Task<Result<CriarPessoaResponse>> Handle(CriarPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = new PessoaEntity()
        {
            Nome = request.Nome,
            Cpf = request.Cpf,
        };

        await _pessoaRepository.CriarPessoaAsync(pessoa);
        return Result.Success(new CriarPessoaResponse(pessoa.Id, pessoa.Nome));
    }
}

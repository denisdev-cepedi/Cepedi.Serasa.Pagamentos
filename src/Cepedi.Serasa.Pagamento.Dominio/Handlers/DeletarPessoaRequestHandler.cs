using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class DeletarPessoaRequestHandler : IRequestHandler<DeletarPessoaRequest, Result<DeletarPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<DeletarPessoaRequestHandler> _logger;
    public DeletarPessoaRequestHandler(ILogger<DeletarPessoaRequestHandler> logger, IPessoaRepository pessoaRepository)
    {
        _logger = logger;
        _pessoaRepository = pessoaRepository;
    }
    public async Task<Result<DeletarPessoaResponse>> Handle(DeletarPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaRepository.ObterPessoaAsync(request.Id);
        if (pessoaEntity == null) return Result.Error<DeletarPessoaResponse>(new Compartilhado.Excecoes.SemResultadosException());
        await _pessoaRepository.DeletarPessoaAsync(pessoaEntity.Id);
        return Result.Success(new DeletarPessoaResponse(pessoaEntity.Id));
    }
}

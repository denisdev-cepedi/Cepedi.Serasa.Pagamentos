using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class ObterPessoaRequestHandler : IRequestHandler<ObterPessoaRequest, Result<ObterPessoaResponse>>
{
    private readonly ILogger<ObterPessoaRequestHandler> _logger;
    private readonly IPessoaRepository _pessoaRepository;
    public ObterPessoaRequestHandler(ILogger<ObterPessoaRequestHandler> logger, IPessoaRepository pessoaRepository)
    {
        _logger = logger;
        _pessoaRepository = pessoaRepository;
    }
    public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaRepository.ObterPessoaAsync(request.Id);
        return pessoaEntity == null
            ? Result.Error<ObterPessoaResponse>(new Compartilhado.Excecoes.SemResultadosException())
            : Result.Success(new ObterPessoaResponse(pessoaEntity.Nome));
    }
}

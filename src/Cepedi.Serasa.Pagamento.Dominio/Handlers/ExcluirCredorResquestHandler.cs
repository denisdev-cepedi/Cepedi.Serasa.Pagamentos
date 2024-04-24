using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class ExcluirCredorRequestHandler :
    IRequestHandler<ExcluirCredorRequest, Result<ExcluirCredorResponse>>
{
    private readonly ICredorRepository _credorRepository;
    private readonly ILogger<ExcluirCredorRequestHandler> _logger;

    public ExcluirCredorRequestHandler(ICredorRepository CredorRepository, ILogger<ExcluirCredorRequestHandler> logger)
    {
        _credorRepository = CredorRepository;
        _logger = logger;
    }

    public async Task<Result<ExcluirCredorResponse>> Handle(ExcluirCredorRequest request, CancellationToken cancellationToken)
    {
        var CredorEntity = await _credorRepository.ObterCredorAsync(request.Id);

        if (CredorEntity == null)
        {
            return Result.Error<ExcluirCredorResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        await _credorRepository.ExcluirCredorAsync(CredorEntity.Id);

        return Result.Success(new ExcluirCredorResponse(CredorEntity.Nome));

    }
}

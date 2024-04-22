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
    private readonly ICredorRepository _CredorRepository;
    private readonly ILogger<ExcluirCredorRequestHandler> _logger;

    public ExcluirCredorRequestHandler(ICredorRepository CredorRepository, ILogger<ExcluirCredorRequestHandler> logger)
    {
        _CredorRepository = CredorRepository;
        _logger = logger;
    }

    public async Task<Result<ExcluirCredorResponse>> Handle(ExcluirCredorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var CredorEntity = await _CredorRepository.ExcluirCredorAsync(request.Id);

            if (CredorEntity == null)
            {
                return Result.Error<ExcluirCredorResponse>(new Compartilhado.
                    Excecoes.SemResultadosException());
            }

            CredorEntity.Excluir(request.Nome);

            await _CredorRepository.ExcluirCredorAsync(CredorEntity);

            return Result.Success(new ExcluirCredorResponse(CredorEntity.Nome));
        }
        catch
        {
            _logger.LogError("Ocorreu um erro ao Excluir os usuários");
            throw;
        }
    }
}

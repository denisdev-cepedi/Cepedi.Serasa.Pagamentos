using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class AtualizarCredorRequestHandler :
    IRequestHandler<AtualizarCredorRequest, Result<AtualizarCredorResponse>>
{
    private readonly ICredorRepository _CredorRepository;
    private readonly ILogger<AtualizarCredorRequestHandler> _logger;

    public AtualizarCredorRequestHandler(ICredorRepository CredorRepository, ILogger<AtualizarCredorRequestHandler> logger)
    {
        _CredorRepository = CredorRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarCredorResponse>> Handle(AtualizarCredorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var CredorEntity = await _CredorRepository.ObterCredorAsync(request.Id);

            if (CredorEntity == null)
            {
                return Result.Error<AtualizarCredorResponse>(new Compartilhado.
                    Excecoes.SemResultadosException());
            }

            CredorEntity.Atualizar(request.Nome);

            await _CredorRepository.AtualizarCredorAsync(CredorEntity);

            return Result.Success(new AtualizarCredorResponse(CredorEntity.Nome));
        }
        catch
        {
            _logger.LogError("Ocorreu um erro ao atualizar os usuários");
            throw;
        }
    }
}

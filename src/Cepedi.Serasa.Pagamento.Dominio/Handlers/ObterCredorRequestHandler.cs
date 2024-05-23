using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class ObterCredorRequestHandler :
    IRequestHandler<ObterCredorRequest, Result<ObterCredorResponse>>
{
    private readonly ICredorRepository _CredorRepository;
    private readonly ILogger<ObterCredorRequestHandler> _logger;

    public ObterCredorRequestHandler(ICredorRepository CredorRepository, ILogger<ObterCredorRequestHandler> logger)
    {
        _CredorRepository = CredorRepository;
        _logger = logger;
    }

    public async Task<Result<ObterCredorResponse>> Handle(ObterCredorRequest request, CancellationToken cancellationToken)
    {

        var CredorEntity = await _CredorRepository.ObterCredorAsync(request.Id);

        if (CredorEntity == null)
        {
            return Result.Error<ObterCredorResponse>(new Compartilhado.
                Excecoes.ExcecaoAplicacao(CredorErros.CredorInexistente));
        }

        return Result.Success(new ObterCredorResponse(CredorEntity.Nome));

    }
}

using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class DeletarCredorRequestHandler :
    IRequestHandler<DeletarCredorRequest, Result<DeletarCredorResponse>>
{
    private readonly ICredorRepository _credorRepository;
    private readonly ILogger<DeletarCredorRequestHandler> _logger;

    public DeletarCredorRequestHandler(ICredorRepository CredorRepository, ILogger<DeletarCredorRequestHandler> logger)
    {
        _credorRepository = CredorRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarCredorResponse>> Handle(DeletarCredorRequest request, CancellationToken cancellationToken)
    {
        var CredorEntity = await _credorRepository.ObterCredorAsync(request.Id);

        if (CredorEntity == null)
        {
            return Result.Error<DeletarCredorResponse>(
                new Compartilhado.Excecoes.ExcecaoAplicacao(CredorErros.CredorInexistente));
        }

        await _credorRepository.DeletarCredorAsync(CredorEntity.Id);

        return Result.Success(new DeletarCredorResponse(CredorEntity.Id));

    }
}

using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class AtualizarCredorRequestHandler :
    IRequestHandler<AtualizarCredorRequest, Result<AtualizarCredorResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICredorRepository _CredorRepository;
    private readonly ILogger<AtualizarCredorRequestHandler> _logger;

    public AtualizarCredorRequestHandler(ICredorRepository CredorRepository, ILogger<AtualizarCredorRequestHandler> logger, IUnitOfWork unitOfWork)
    {
        _CredorRepository = CredorRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    //modificar
    public async Task<Result<AtualizarCredorResponse>> Handle(AtualizarCredorRequest request, CancellationToken cancellationToken)
    {

        var CredorEntity = await _CredorRepository.ObterCredorAsync(request.Id);

        if (CredorEntity == null)
        {
            return Result.Error<AtualizarCredorResponse>(
                new Compartilhado.Excecoes.ExcecaoAplicacao(CredorErros.CredorInexistente));
        }

        CredorEntity.Atualizar(request.Nome);

        await _CredorRepository.AtualizarCredorAsync(CredorEntity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new AtualizarCredorResponse(CredorEntity.Nome));
    }
}

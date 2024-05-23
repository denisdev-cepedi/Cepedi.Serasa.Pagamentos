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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICredorRepository _credorRepository;
    private readonly ILogger<ExcluirCredorRequestHandler> _logger;

    public ExcluirCredorRequestHandler(ICredorRepository CredorRepository, ILogger<ExcluirCredorRequestHandler> logger, IUnitOfWork unitOfWork)
    {
        _credorRepository = CredorRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.SaveChangesAsync(cancellationToken);        

        return Result.Success(new ExcluirCredorResponse(CredorEntity.Id));

    }
}

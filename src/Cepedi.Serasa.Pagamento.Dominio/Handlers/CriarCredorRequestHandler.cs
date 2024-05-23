using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class CriarCredorRequestHandler
    : IRequestHandler<CriarCredorRequest, Result<CriarCredorResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CriarCredorRequestHandler> _logger;
    private readonly ICredorRepository _CredorRepository;

    public CriarCredorRequestHandler(ICredorRepository CredorRepository, ILogger<CriarCredorRequestHandler> logger, IUnitOfWork unitOfWork)
    {
        _CredorRepository = CredorRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CriarCredorResponse>> Handle(CriarCredorRequest request, CancellationToken cancellationToken)
    {

        var Credor = new CredorEntity()
        {
            Nome = request.Nome,
        };

        await _CredorRepository.CriarCredorAsync(Credor);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new CriarCredorResponse(Credor.Id, Credor.Nome));

    }
}

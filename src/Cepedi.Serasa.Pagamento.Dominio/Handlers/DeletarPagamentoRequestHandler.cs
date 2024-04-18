using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class DeletarPagamentoRequestHandler :
    IRequestHandler<DeletarPagamentoRequest, Result<DeletarPagamentoResponse>>
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly ILogger<DeletarPagamentoRequestHandler> _logger;

    public DeletarPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ILogger<DeletarPagamentoRequestHandler> logger)
    {
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarPagamentoResponse>> Handle(DeletarPagamentoRequest request, CancellationToken cancellationToken)
    {
        var pagamentoEntity = await _pagamentoRepository.ObterPagamentoAsync(request.Id);

        if (pagamentoEntity == null)
        {
            return Result.Error<DeletarPagamentoResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        await _pagamentoRepository.DeletarPagamentoAsync(pagamentoEntity.Id);

        return Result.Success(new DeletarPagamentoResponse(pagamentoEntity.Id));
    }
}

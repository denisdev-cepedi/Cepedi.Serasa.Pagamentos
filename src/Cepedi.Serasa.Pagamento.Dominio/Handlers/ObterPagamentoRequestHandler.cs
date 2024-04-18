using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class ObterPagamentoRequestHandler :
    IRequestHandler<ObterPagamentoRequest, Result<ObterPagamentoResponse>>
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly ILogger<ObterPagamentoRequestHandler> _logger;

    public ObterPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ILogger<ObterPagamentoRequestHandler> logger)
    {
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
    }

    public async Task<Result<ObterPagamentoResponse>> Handle(ObterPagamentoRequest request, CancellationToken cancellationToken)
    {
        var pagamentoEntity = await _pagamentoRepository.ObterPagamentoAsync(request.Id);

        if (pagamentoEntity == null)
        {
            return Result.Error<ObterPagamentoResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        return Result.Success(new ObterPagamentoResponse(pagamentoEntity.Valor));

    }
}

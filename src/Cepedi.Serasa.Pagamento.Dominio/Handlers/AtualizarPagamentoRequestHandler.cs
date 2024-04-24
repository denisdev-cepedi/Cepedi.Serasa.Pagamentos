using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class AtualizarPagamentoRequestHandler :
    IRequestHandler<AtualizarPagamentoRequest, Result<AtualizarPagamentoResponse>>
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly ILogger<AtualizarPagamentoRequestHandler> _logger;

    public AtualizarPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ILogger<AtualizarPagamentoRequestHandler> logger)
    {
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarPagamentoResponse>> Handle(AtualizarPagamentoRequest request, CancellationToken cancellationToken)
    {
        var pagamentoEntity = await _pagamentoRepository.ObterPagamentoAsync(request.Id);

        if (pagamentoEntity == null)
        {
            return Result.Error<AtualizarPagamentoResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        pagamentoEntity.AtualizarValor(request.Valor);

        await _pagamentoRepository.AtualizarPagamentoAsync(pagamentoEntity);

        return Result.Success(new AtualizarPagamentoResponse(pagamentoEntity.Valor));
    }
}

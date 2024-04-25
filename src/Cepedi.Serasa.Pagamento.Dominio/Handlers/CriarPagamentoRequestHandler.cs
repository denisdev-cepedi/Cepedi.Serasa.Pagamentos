using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class CriarPagamentoRequestHandler
    : IRequestHandler<CriarPagamentoRequest, Result<CriarPagamentoResponse>>
{
    private readonly ILogger<CriarPagamentoRequestHandler> _logger;
    private readonly ICredorRepository _credorRepository;
    private readonly IPagamentoRepository _pagamentoRepository;

    public CriarPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ICredorRepository credorRepository, ILogger<CriarPagamentoRequestHandler> logger)
    {
        _credorRepository = credorRepository;
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
    }

    public async Task<Result<CriarPagamentoResponse>> Handle(CriarPagamentoRequest request, CancellationToken cancellationToken)
    {
        var credorEntity = await _credorRepository.ObterCredorAsync(request.IdCredor);

        var pagamento = new PagamentoEntity()
        {
            Valor = request.Valor,

            DataDePagamento = request.DataDePagamento,

            DataDeVencimento = request.DataDeVencimento,

            IdCredor = request.IdCredor,

            Credor = credorEntity
        };

        await _pagamentoRepository.CriarPagamentoAsync(pagamento);

        return Result.Success(new CriarPagamentoResponse(pagamento.Id, pagamento.Valor));
    }
}

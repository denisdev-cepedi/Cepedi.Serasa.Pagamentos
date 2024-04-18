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
    private readonly IPagamentoRepository _pagamentoRepository;

    public CriarPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ILogger<CriarPagamentoRequestHandler> logger)
    {
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
    }

    public async Task<Result<CriarPagamentoResponse>> Handle(CriarPagamentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var credorEntity = await _pagamentoRepository.ObterCredorPagamentoAsync(request.IdCredor);

            var pagamento = new PagamentoEntity()
            {
                Id = request.Id,

                Valor = request.Valor,

                DataDePagamento = request.DataDePagamento,

                DataDeVencimento = request.DataDeVencimento,

                IdCredor = request.IdCredor,

                Credor = credorEntity
            };

            await _pagamentoRepository.CriarPagamentoAsync(pagamento);

            return Result.Success(new CriarPagamentoResponse(pagamento.Id, pagamento.Valor));
        }
        catch
        {
            _logger.LogError("Ocorreu um erro durante a execução");
            return Result.Error<CriarPagamentoResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(
                (PagamentoErros.ErroAoEfetuarPagamento)));
        }
    }
}

using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class CriarPagamentoRequestHandler
    : IRequestHandler<CriarPagamentoRequest, Result<CriarPagamentoResponse>>
{
    private readonly ILogger<CriarPagamentoRequestHandler> _logger;
    private readonly ICredorRepository _credorRepository;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IDividaRepository _dividaRepository;

    public CriarPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ICredorRepository credorRepository, IDividaRepository dividaRepository, ILogger<CriarPagamentoRequestHandler> logger)
    {
        _credorRepository = credorRepository;
        _pagamentoRepository = pagamentoRepository;
        _dividaRepository = dividaRepository;
        _logger = logger;
    }

    public async Task<Result<CriarPagamentoResponse>> Handle(CriarPagamentoRequest request, CancellationToken cancellationToken)
    {
        var credorEntity = await _credorRepository.ObterCredorAsync(request.IdCredor);

        var dividaEntity = await _dividaRepository.ObterDividaAsync(request.IdDivida);

        var pagamento = new PagamentoEntity()
        {
            Valor = request.Valor,

            DataDePagamento = request.DataDePagamento,

            DataDeVencimento = request.DataDeVencimento,

            IdCredor = request.IdCredor,

            Credor = credorEntity
        };

        if (dividaEntity == null)
        {
            return Result.Error<CriarPagamentoResponse>(
            new Compartilhado.Excecoes.ExcecaoAplicacao(DividaErros.DividaNaoEncontrada));
        }

        if (dividaEntity.Valor != pagamento.Valor)
        {
            return Result.Error<CriarPagamentoResponse>(
            new Compartilhado.Excecoes.ExcecaoAplicacao(PagamentoErros.ValorPagamentoDiferente));
        }

        dividaEntity.DividaAberta = false;

        if (null == await _pagamentoRepository.QuitarPagamentoAsync(dividaEntity))
        {
            return Result.Error<CriarPagamentoResponse>(
            new Compartilhado.Excecoes.SemResultadosException());
        }

        await _pagamentoRepository.CriarPagamentoAsync(pagamento);


        return Result.Success(new CriarPagamentoResponse(pagamento.Id, pagamento.Valor));
    }
}

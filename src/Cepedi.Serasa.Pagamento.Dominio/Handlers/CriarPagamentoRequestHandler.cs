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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CriarPagamentoRequestHandler> _logger;
    private readonly ICredorRepository _credorRepository;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IDividaRepository _dividaRepository;

    public CriarPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ICredorRepository credorRepository, IDividaRepository dividaRepository, ILogger<CriarPagamentoRequestHandler> logger, IUnitOfWork unitOfWork)
    {
        _credorRepository = credorRepository;
        _pagamentoRepository = pagamentoRepository;
        _dividaRepository = dividaRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CriarPagamentoResponse>> Handle(CriarPagamentoRequest request, CancellationToken cancellationToken)
    {

        var dividaEntity = await _dividaRepository.ObterDividaAsync(request.IdDivida);

        var credorEntity = await _credorRepository.ObterCredorAsync(dividaEntity.IdCredor);

        var pagamento = new PagamentoEntity()
        {
            Valor = request.Valor,

            DataDePagamento = request.DataDePagamento,

            DataDeVencimento = request.DataDeVencimento,

            IdCredor = dividaEntity.IdCredor,

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
            new Compartilhado.Excecoes.ExcecaoAplicacao(PagamentoErros.ValorPagamentoIncompativelComDivida));
        }

        dividaEntity.DividaAberta = false;

        if (null == await _pagamentoRepository.QuitarPagamentoAsync(dividaEntity))
        {
            return Result.Error<CriarPagamentoResponse>(
            new Compartilhado.Excecoes.SemResultadosException());
        }

        await _pagamentoRepository.CriarPagamentoAsync(pagamento);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(new CriarPagamentoResponse(pagamento.Id, pagamento.Valor));
    }
}



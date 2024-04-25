using Cepedi.Serasa.Pagamento.Compartilhado;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class AtualizarDividaRequestHandler : IRequestHandler<AtualizarDividaRequest, Result<AtualizarDividaResponse>>
{
    private readonly IDividaRepository _dividaRepository;
    private readonly ILogger<AtualizarDividaRequestHandler> _logger;

    public AtualizarDividaRequestHandler(IDividaRepository dividaRepository, ILogger<AtualizarDividaRequestHandler> logger)
    {
        _dividaRepository = dividaRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarDividaResponse>> Handle(AtualizarDividaRequest request, CancellationToken cancellationToken)
    {
        var dividaEntity = await _dividaRepository.ObterDividaAsync(request.Id);

        if (dividaEntity == null)
        {
            return Result.Error<AtualizarDividaResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        dividaEntity.AtualizarDados(request.Valor, request.DataDeVencimento);

        await _dividaRepository.AtualizarDividaAsync(dividaEntity);

        return Result.Success(new AtualizarDividaResponse(dividaEntity.Valor, dividaEntity.DataDeVencimento));
    }

}

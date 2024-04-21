using Cepedi.Serasa.Pagamento.Compartilhado;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class ObtemDividaResquestHandler : IRequestHandler<ObtemDividaRequest, Result<ObtemDividaResponse>>
{
    private readonly IDividaRepository _dividaRepository;
    private readonly ILogger<ObtemDividaResquestHandler> _logger;

    public ObtemDividaResquestHandler(IDividaRepository dividaRepository, ILogger<ObtemDividaResquestHandler> logger)
    {
        _dividaRepository = dividaRepository;
        _logger = logger;
    }

public async Task<Result<ObtemDividaResponse>> Handle(ObtemDividaRequest request, CancellationToken cancellationToken)
    {
        var dividaEntity = await _dividaRepository.ObterDividaAsync(request.Id);

        if (dividaEntity == null)
        {
            return Result.Error<ObtemDividaResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        return Result.Success(new ObtemDividaResponse(dividaEntity.Valor, dividaEntity.DataDeVencimento));

    }
}

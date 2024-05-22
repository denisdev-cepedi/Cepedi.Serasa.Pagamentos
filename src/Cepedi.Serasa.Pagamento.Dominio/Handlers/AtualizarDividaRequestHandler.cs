using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
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
                Excecoes.ExcecaoAplicacao(DividaErros.DividaNaoEncontrada));
        }

        dividaEntity.AtualizarDados(request.Valor, request.DataDeVencimento);

        var response = await _dividaRepository.AtualizarDividaAsync(dividaEntity);

        if(response == null){
            return Result.Error<AtualizarDividaResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(DividaErros.ErroGravacaoDivida));
        }

        return Result.Success(new AtualizarDividaResponse(dividaEntity.Valor, dividaEntity.DataDeVencimento));
    }

}

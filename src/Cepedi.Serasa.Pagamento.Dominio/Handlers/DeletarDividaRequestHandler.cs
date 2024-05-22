using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class DeletarDividaRequestHandler : IRequestHandler<DeletarDividaRequest, Result<DeletarDividaResponse>>
{
    private readonly IDividaRepository _dividaRepository;
    private readonly ILogger<DeletarDividaRequestHandler> _logger;

    public DeletarDividaRequestHandler(IDividaRepository dividaRepository, ILogger<DeletarDividaRequestHandler> logger)
    {
        _dividaRepository = dividaRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarDividaResponse>> Handle(DeletarDividaRequest request, CancellationToken cancellationToken)
    {
        var dividaEntity = await _dividaRepository.ObterDividaAsync(request.Id);

        if (dividaEntity == null)
        {
            return Result.Error<DeletarDividaResponse>(new Compartilhado.
                Excecoes.ExcecaoAplicacao(DividaErros.Generico));
        }

        var response = await _dividaRepository.DeletarDividaAsync(dividaEntity.Id);


        if(response == null){
            return Result.Error<DeletarDividaResponse>(
                new Compartilhado.Excecoes.ExcecaoAplicacao(DividaErros.ErroGravacaoDivida));
        }

        return Result.Success(new DeletarDividaResponse(dividaEntity.Id));
    }

}


using System.Linq;
using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class ObtemDividasRequestHandler : IRequestHandler<ObtemDividasRequest, Result<ObtemDividasResponse>>
{
      private readonly IDividaRepository _dividaRepository;
    private readonly ILogger<ObtemDividasRequestHandler> _logger;

    public ObtemDividasRequestHandler(IDividaRepository dividaRepository, ILogger<ObtemDividasRequestHandler> logger)
    {
        _dividaRepository = dividaRepository;
        _logger = logger;
    }

public async Task<Result<ObtemDividasResponse>> Handle(ObtemDividasRequest request, CancellationToken cancellationToken)
    {
        var dividaEntity = await _dividaRepository.ObterDividasPessoaAsync(request.Id);

        if (dividaEntity == null)
        {
            return Result.Error<ObtemDividasResponse>(new Compartilhado.
                Excecoes.ExcecaoAplicacao(DividaErros.SemResultados));
        }

        return Result.Success(new ObtemDividasResponse(dividaEntity.Select(x => new ObtemDividaResponse(x.Valor, x.DataDeVencimento)).ToList()));

    }

   
}



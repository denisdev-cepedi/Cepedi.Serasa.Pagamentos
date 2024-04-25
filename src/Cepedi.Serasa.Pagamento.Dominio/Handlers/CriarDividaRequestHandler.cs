using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using MediatR;
using OperationResult;
using Microsoft.Extensions.Logging;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class CriarDividaRequestHandler
    : IRequestHandler<CriarDividaRequest, Result<CriarDividaResponse>>
{
    private readonly ILogger _logger;
    private readonly IDividaRepository _dividaRepository;

    private readonly ICredorRepository _credorRepository;

    public CriarDividaRequestHandler(ILogger logger, IDividaRepository dividaRepository, ICredorRepository credorRepository){
        _logger = logger;
        _dividaRepository = dividaRepository;
        _credorRepository = credorRepository;
    }
    public async Task<Result<CriarDividaResponse>> Handle(CriarDividaRequest request, CancellationToken cancellationToken)
    {
        var credorEntity = await _credorRepository.ObterCredorAsync(request.IdCredor);
        var pessoaEntity = await _dividaRepository.ObterPessoaAsync(request.IdPessoa);
        var divida = new DividaEntity()
        {
            IdCredor = request.IdCredor,
            IdPessoa = request.IdPessoa,
            DataDeVencimento = request.DataDeVencimento,
            Credor = credorEntity,
            Valor = request.Valor,
            Pessoa = pessoaEntity
        };

        await _dividaRepository.CriarDividaAsync(divida);
        return Result.Success(new CriarDividaResponse(divida.Id, divida.Valor, divida.DataDeVencimento));
    }
}

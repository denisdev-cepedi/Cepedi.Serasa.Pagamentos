using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using MediatR;
using OperationResult;
using Microsoft.Extensions.Logging;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class CriarDividaRequestHandler
    : IRequestHandler<CriarDividaRequest, Result<CriarDividaResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CriarDividaRequestHandler> _logger;
    private readonly IDividaRepository _dividaRepository;

    private readonly IPessoaRepository _pessoaRepository;

    private readonly ICredorRepository _credorRepository;

    public CriarDividaRequestHandler(ILogger<CriarDividaRequestHandler> logger, IDividaRepository dividaRepository, ICredorRepository credorRepository, IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _dividaRepository = dividaRepository;
        _credorRepository = credorRepository;
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<CriarDividaResponse>> Handle(CriarDividaRequest request, CancellationToken cancellationToken)
    {
        var credorEntity = await _credorRepository.ObterCredorAsync(request.IdCredor);
        var pessoaEntity = await _pessoaRepository.ObterPessoaAsync(request.IdPessoa);

        if(credorEntity == null || pessoaEntity == null){
            return Result.Error<CriarDividaResponse>(
            new Compartilhado.Excecoes.ExcecaoAplicacao(DividaErros.DadosInvalidos));
        }

        var divida = new DividaEntity()
        {
            IdCredor = request.IdCredor,
            IdPessoa = request.IdPessoa,
            DataDeVencimento = request.DataDeVencimento,
            Credor = credorEntity,
            Valor = request.Valor,
            Pessoa = pessoaEntity
        };
        

        var response = await _dividaRepository.CriarDividaAsync(divida);

        if(response == null){
            return Result.Error<CriarDividaResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(DividaErros.ErroGravacaoDivida));
        }


        await _dividaRepository.CriarDividaAsync(divida);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new CriarDividaResponse(divida.Id, divida.Valor, divida.DataDeVencimento));
    }
}

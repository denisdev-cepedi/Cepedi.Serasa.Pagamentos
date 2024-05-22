using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
using Cepedi.Serasa.Pagamento.Dominio.Servicos;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio;

public class ObterPessoaRequestHandler : IRequestHandler<ObterPessoaRequest, Result<ObterPessoaResponse>>
{
    private readonly ILogger<ObterPessoaRequestHandler> _logger;
    private readonly IPessoaRepository _pessoaRepository;

    private readonly ICache<PessoaEntity> _cache;

    private readonly IPessoaQueryRepository _pessoaQueryRepository;
    public ObterPessoaRequestHandler(ILogger<ObterPessoaRequestHandler> logger, ICache<PessoaEntity> cache, IPessoaQueryRepository pessoaQueryRepository, IPessoaRepository pessoaRepository)
    {
        _logger = logger;
        _pessoaRepository = pessoaRepository;
        _pessoaQueryRepository = pessoaQueryRepository;
        _cache = cache;
    }
    public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaRequest request, CancellationToken cancellationToken)
    {

        var pessoaEntity = await _cache.ObterAsync($"Pessoa:{request.Id}");

        if (pessoaEntity != null)
        {
            return Result.Success(new ObterPessoaResponse(pessoaEntity.Nome));
        }
        else
        {
            pessoaEntity = await _pessoaQueryRepository.ObterPessoasDapperAsync(request.Id);
            if (pessoaEntity == null) return Result.Error<ObterPessoaResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(PessoaErros.PessoaNaoEncontrada));
            await _cache.SalvarAsync($"Pessoa:{pessoaEntity.Id}", pessoaEntity, 1800);
            return Result.Success(new ObterPessoaResponse(pessoaEntity.Nome));

        }
    }
}

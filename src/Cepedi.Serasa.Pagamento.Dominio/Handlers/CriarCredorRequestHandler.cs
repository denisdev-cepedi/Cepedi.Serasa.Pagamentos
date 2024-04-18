﻿using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class CriarCredorRequestHandler 
    : IRequestHandler<CriarCredorRequest, Result<CriarCredorResponse>>
{
    private readonly ILogger<CriarCredorRequestHandler> _logger;
    private readonly ICredorRepository _CredorRepository;

    public CriarCredorRequestHandler(ICredorRepository CredorRepository, ILogger<CriarCredorRequestHandler> logger)
    {
        _CredorRepository = CredorRepository;
        _logger = logger;
    }

    public async Task<Result<CriarCredorResponse>> Handle(CriarCredorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var Credor = new CredorEntity()
            {
                Nome = request.Nome,
            };

            await _CredorRepository.CriarCredorAsync(Credor);

            return Result.Success(new CriarCredorResponse(Credor.Id, Credor.Nome));
        }
        catch
        {
            _logger.LogError("Ocorreu um erro durante a execução");
            return Result.Error<CriarCredorResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(
                PagamentoErros.ErroGravacaoCredor));
        }
    }
}

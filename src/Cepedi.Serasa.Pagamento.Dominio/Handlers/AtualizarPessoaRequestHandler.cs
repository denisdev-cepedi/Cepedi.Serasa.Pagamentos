﻿using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;

public class AtualizarPessoaRequestHandler : IRequestHandler<AtualizarPessoaRequest, Result<AtualizarPessoaResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPessoaQueryRepository _pessoaQueryRepository;

    private readonly IPessoaRepository _pessoaRepository;

    private readonly ILogger<AtualizarPessoaRequestHandler> _logger;
    public AtualizarPessoaRequestHandler(IPessoaRepository pessoaRepository, IPessoaQueryRepository pessoaQueryRepository, IUnitOfWork unitOfWork, ILogger<AtualizarPessoaRequestHandler> logger)
    {
        _pessoaQueryRepository = pessoaQueryRepository;
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result<AtualizarPessoaResponse>> Handle(AtualizarPessoaRequest request, CancellationToken cancellationToken)
    {

        var pessoaEntity = await _pessoaQueryRepository.ObterPessoasDapperAsync(request.Id);

        if (pessoaEntity == null)
        {
            return Result.Error<AtualizarPessoaResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        pessoaEntity.Atualizar(request.Nome);

        await _pessoaQueryRepository.AtualizarPessoaDapperAsync(pessoaEntity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new AtualizarPessoaResponse(pessoaEntity.Nome));

    }
}

﻿using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class AtualizarPagamentoRequestHandler :
    IRequestHandler<AtualizarPagamentoRequest, Result<AtualizarPagamentoResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly ILogger<AtualizarPagamentoRequestHandler> _logger;

    public AtualizarPagamentoRequestHandler(IPagamentoRepository pagamentoRepository, ILogger<AtualizarPagamentoRequestHandler> logger, IUnitOfWork unitOfWork)
    {
        _pagamentoRepository = pagamentoRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AtualizarPagamentoResponse>> Handle(AtualizarPagamentoRequest request, CancellationToken cancellationToken)
    {
        var pagamentoEntity = await _pagamentoRepository.ObterPagamentoAsync(request.Id);

        if (pagamentoEntity == null)
        {
            return Result.Error<AtualizarPagamentoResponse>(
             new Compartilhado.Excecoes.ExcecaoAplicacao(PagamentoErros.PagamentoNaoEncontrado));
        }

        pagamentoEntity.AtualizarValor(request.Valor);

        await _pagamentoRepository.AtualizarPagamentoAsync(pagamentoEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new AtualizarPagamentoResponse(pagamentoEntity.Valor));
    }
}

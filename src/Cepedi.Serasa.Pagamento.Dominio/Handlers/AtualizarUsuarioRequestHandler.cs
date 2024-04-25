﻿using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Handlers;
public class AtualizarUsuarioRequestHandler :
    IRequestHandler<AtualizarUsuarioRequest, Result<AtualizarUsuarioResponse>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<AtualizarUsuarioRequestHandler> _logger;

    public AtualizarUsuarioRequestHandler(IUsuarioRepository usuarioRepository, ILogger<AtualizarUsuarioRequestHandler> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarUsuarioResponse>> Handle(AtualizarUsuarioRequest request, CancellationToken cancellationToken)
    {

        var usuarioEntity = await _usuarioRepository.ObterUsuarioAsync(request.Id);

        if (usuarioEntity == null)
        {
            return Result.Error<AtualizarUsuarioResponse>(new Compartilhado.
                Excecoes.SemResultadosException());
        }

        usuarioEntity.Atualizar(request.Nome);

        await _usuarioRepository.AtualizarUsuarioAsync(usuarioEntity);

        return Result.Success(new AtualizarUsuarioResponse(usuarioEntity.Nome));
    }

}

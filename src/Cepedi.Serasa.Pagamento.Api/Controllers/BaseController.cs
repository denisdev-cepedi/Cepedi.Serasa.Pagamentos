﻿using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Api.Controllers;
public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<ActionResult> SendCommand(IRequest<Result> request, int statusCode = 200)
        => await _mediator.Send(request) switch
        {
            (true, _) => StatusCode(statusCode),
            var (_, error) => HandleError(error!)
        };

    protected async Task<ActionResult<T>> SendCommand<T>(IRequest<Result<T>> request, int statusCode = 200)
        => await _mediator.Send(request).ConfigureAwait(false) switch
        {
            (true, var result, _) => StatusCode(statusCode, result),
            var (_, res, error) => HandleError(error!)
        };

    protected ActionResult HandleError(Exception error)
    {
        if (error is ExcecaoAplicacao excecao)
        {
            return BadRequest(FormatErrorMessage(excecao.ResponseErro));
        }
        else
        {
            // Se a exceção não for do tipo ExcecaoAplicacao, retorna um erro genérico
            return BadRequest(FormatErrorMessage(CadastroErros.Generico));
        }
    }

    private ResultadoErro FormatErrorMessage(ResultadoErro responseErro, IEnumerable<string>? errors = null)
    {
        if (errors != null)
        {
            responseErro.Descricao = $"{responseErro.Descricao}: {string.Join("; ", errors!)}";
        }

        return responseErro;
    }
}

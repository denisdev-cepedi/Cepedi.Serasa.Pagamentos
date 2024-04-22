using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Pagamento.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PagamentoController : BaseController
{
    private readonly ILogger<PagamentoController> _logger;
    private readonly IMediator _mediator;

    public PagamentoController(
        ILogger<PagamentoController> logger, IMediator mediator)
        : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarPagamentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarPagamentoResponse>> CriarPagamentoAsync(
        [FromBody] CriarPagamentoRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarPagamentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarPagamentoResponse>> AtualizarPagamentoAsync(
        [FromBody] AtualizarPagamentoRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ObterPagamentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterPagamentoResponse>> ObterPagamentoAsync(
        [FromRoute] ObterPagamentoRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeletarPagamentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarPagamentoResponse>> DeletarPagamentoAsync(
        [FromRoute] DeletarPagamentoRequest request) => await SendCommand(request);

}

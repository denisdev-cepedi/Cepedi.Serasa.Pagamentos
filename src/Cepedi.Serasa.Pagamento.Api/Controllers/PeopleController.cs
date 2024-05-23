using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Pagamento.Api.Controllers;

[ApiController]
[Route("[controller]")]

[Authorize]
public class PessoaController : BaseController
{
    private readonly ILogger<PessoaController> _logger;
    private readonly IMediator _mediator;
    public PessoaController(ILogger<PessoaController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarPessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarPessoaResponse>> CriarPessoaAsync([FromBody] CriarPessoaRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarPessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarPessoaResponse>> AtualizarPessoaAsync(
        [FromBody] AtualizarPessoaRequest request) => await SendCommand(request);

    // [HttpGet]
    // [ProducesResponseType(typeof(ObterPessoasResponse), StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<ObterPessoasResponse>> ObterPessoasAsync(
    //     [FromRoute] ObterPessoasRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ObterPessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterPessoaResponse>> ObterPessoaAsync(
        [FromRoute] ObterPessoaRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeletarPessoaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarPessoaResponse>> DeletarPessoaAsync(
        [FromRoute] DeletarPessoaRequest request) => await SendCommand(request);
}

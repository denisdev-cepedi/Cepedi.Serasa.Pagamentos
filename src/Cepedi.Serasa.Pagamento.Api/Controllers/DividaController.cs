using Cepedi.Serasa.Pagamento.Api.Controllers;
using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Pagamento.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DividaController : BaseController
{
    private readonly ILogger<DividaController> _logger;
    private readonly IMediator _mediator;

    public DividaController(ILogger<DividaController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarDividaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarDividaResponse>> CriarDividaAsync(
        [FromBody] CriarDividaRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarDividaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarDividaResponse>> AtualizarDividaAsync(
        [FromBody] AtualizarDividaRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ObtemDividaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObtemDividaResponse>> ObterDividaAsync(
        [FromRoute] ObtemDividaRequest request) => await SendCommand(request);

    [HttpGet("Pessoa/{Id}")]
    [ProducesResponseType(typeof(ObtemDividasResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObtemDividasResponse>> ObterDividasPessoaAsync(
        [FromRoute] ObtemDividasRequest request) => await SendCommand(request);
        

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeletarDividaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarDividaResponse>> DeletarDividaAsync(
        [FromRoute] DeletarDividaRequest request) => await SendCommand(request);
}

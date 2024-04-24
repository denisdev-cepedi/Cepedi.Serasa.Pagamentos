using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Pagamento.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CredorController : BaseController
{
    private readonly ILogger<CredorController> _logger;
    private readonly IMediator _mediator;

    public CredorController(
        ILogger<CredorController> logger, IMediator mediator)
        : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }


    [HttpPost]
    [ProducesResponseType(typeof(CriarCredorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarCredorResponse>> CriarCredorAsync(
        [FromBody] CriarCredorRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarCredorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarCredorResponse>> AtualizarCredorAsync(
        [FromBody] AtualizarCredorRequest request) => await SendCommand(request);

    [HttpGet]
    [ProducesResponseType(typeof(ObterCredorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterCredorResponse>> ObterCredorAsync(
        [FromQuery] ObterCredorRequest request) => await SendCommand(request);

    [HttpDelete]
    [ProducesResponseType(typeof(ExcluirCredorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExcluirCredorResponse>> ExcluirCredorAsync(
        [FromQuery] ExcluirCredorRequest request) => await SendCommand(request);

}

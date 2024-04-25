using Castle.Core.Logging;
using Cepedi.Serasa.Pagamento.Api.Controllers;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Api.Tests;

public class PessoaControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<PessoaController> _logger = Substitute.For<ILogger<PessoaController>>();
    private readonly PessoaController _sut;

    public PessoaControllerTests(){
        _sut = new PessoaController(_logger, _mediator);
    }

    [Fact]
    public async Task CriarPessoa_DeveEnviarRequest_Para_Mediator()
    {
        var request = new CriarPessoaRequest { Nome = "Eduardo"};
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarPessoaResponse(1, "")));

        await _sut.CriarPessoaAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }
}

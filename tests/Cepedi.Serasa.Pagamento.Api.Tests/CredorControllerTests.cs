using Castle.Core.Logging;
using Cepedi.Serasa.Pagamento.Api.Controllers;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Api.Tests;

public class CredorControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<CredorController> _logger = Substitute.For<ILogger<CredorController>>();
    private readonly CredorController _sut;

    public CredorControllerTests(){
        _sut = new CredorController(_logger, _mediator);
    }

    [Fact]
    public async Task CriarCredor_DeveEnviarRequest_Para_Mediator()
    {
        var request = new CriarCredorRequest { Nome = "Wilton"};
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarCredorResponse(1, "")));

        await _sut.CriarCredorAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }
}


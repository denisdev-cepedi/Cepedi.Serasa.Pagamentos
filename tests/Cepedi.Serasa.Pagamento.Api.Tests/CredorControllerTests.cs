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

    public CredorControllerTests()
    {
        _sut = new CredorController(_logger, _mediator);
    }

    // Test to create a Credor
    [Fact]
    public async Task Create_Credor_ReturnsCreatedResult()
    {
        // Arrange
        var request = new CriarCredorRequest { Nome = "Joao" };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarCredorResponse(1, "")));
        // Act
        await _sut.CriarCredorAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task AtualizarCredor_DeveRetornarSucesso()
    {
        var request = new AtualizarCredorRequest { Id = 1, Nome = "NovoNome" };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new AtualizarCredorResponse("")));

        await _sut.AtualizarCredorAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task ObterCredor_DeveRetornarSucesso()
    {
        var request = new ObterCredorRequest { Id = 1 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new ObterCredorResponse("")));

        await _sut.ObterCredorAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task DeletarCredor_DeveRetornarSucesso()
    {
        var request = new DeletarCredorRequest { Id = 1 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new DeletarCredorResponse("")));

        await _sut.DeletarCredorAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }
}

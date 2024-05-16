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

    public PessoaControllerTests()
    {
        _sut = new PessoaController(_logger, _mediator);
    }

    [Fact]
    public async Task CriarPessoa_DeveEnviarRequest_Para_Mediator()
    {
        var request = new CriarPessoaRequest { Nome = "Eduardo" };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarPessoaResponse(1, "")));

        await _sut.CriarPessoaAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task AtualizarPessoa_DeveEnviarRequest_Para_Mediator()
    {
        // Arrange
        var request = new AtualizarPessoaRequest { Nome = "João" };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new AtualizarPessoaResponse("")));

        // Act
        await _sut.AtualizarPessoaAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task GetById_DeveEnviarRequest_Para_Mediator()
    {
        var request = new ObterPessoaRequest { Id = 10 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new ObterPessoaResponse("")));

        await _sut.ObterPessoaAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task Delete_DeveEnviarRequest_Para_Mediator()
    {
        var request = new DeletarPessoaRequest { Id = 5 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new DeletarPessoaResponse(5)));

        await _sut.DeletarPessoaAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }
}

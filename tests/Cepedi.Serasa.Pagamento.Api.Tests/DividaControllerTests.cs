using Cepedi.Serasa.Pagamento.Api.Controllers;
using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Api.Tests;

public class DividaControllerTests
{
    private readonly ILogger<DividaController> _logger = Substitute.For<ILogger<DividaController>>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly DividaController _sut;

    public DividaControllerTests()
    {
        _sut = new DividaController(_logger, _mediator);
    }

    [Fact]
    public async Task CriarDivida_DeveEnviarRequest_Para_Mediator()
    {
        // Arrange
        var request = new CriarDividaRequest { Valor = 100, DataDeVencimento = DateTime.Now, IdPessoa = 1, IdCredor = 1 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarDividaResponse(1, 100, DateTime.Now)));
        // Act
        await _sut.CriarDividaAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task AtualizarDivida_DeveEnviarRequest_Para_Mediator()
    {
        var request = new AtualizarDividaRequest { Id = 5, Valor = 200, DataDeVencimento = DateTime.Now };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new AtualizarDividaResponse(200, DateTime.Now)));

        await _sut.AtualizarDividaAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task ObterDivida_DeveEnviarRequest_Para_Mediator()
    {
        var request = new ObtemDividaRequest { Id = 10 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new ObtemDividaResponse(200, DateTime.Now)));

        await _sut.ObterDividaAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task DeletarDivida_DeveEnviarRequest_Para_Mediator()
    {
        var request = new DeletarDividaRequest { Id = 15 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new DeletarDividaResponse(15)));

        await _sut.DeletarDividaAsync(request);

        await _mediator.ReceivedWithAnyArgs().Send(request);
    }
}

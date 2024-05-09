using Castle.Core.Logging;
using Cepedi.Serasa.Pagamento.Api.Controllers;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Api.Tests;

public class DividaControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<DividaController> _logger = Substitute.For<ILogger<DividaController>>();
    private readonly DividaController _sut;

    public DividaControllerTests(){
        _sut = new DividaController(_logger, _mediator);
    }

    [Fact]
    public async Task CriarDivida_DeveEnviarRequest_Para_Mediator(){
        
        // Arrange
        var request = new CriarDividaRequest {Valor = 190};
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarDividaResponse(1, 190, DateTime.Now)));

        // Act
        await _sut.CriarDividaAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);

    }

}

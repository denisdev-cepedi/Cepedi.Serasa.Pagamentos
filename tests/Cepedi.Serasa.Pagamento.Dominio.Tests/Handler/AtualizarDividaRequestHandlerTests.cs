using Castle.Core.Logging;
using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class AtualizarDividaRequestHandlerTests
{
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly ILogger<AtualizarDividaRequestHandler> _logger = Substitute.For<ILogger<AtualizarDividaRequestHandler>>();
    private readonly AtualizarDividaRequestHandler _sut;

    public AtualizarDividaRequestHandlerTests(){
        _sut = new AtualizarDividaRequestHandler (_dividaRepository, _logger);
    }

    [Fact]
    public async Task AtualizarDividaAsync_QuandoCriar_DeveRetornarSucesso()
    {
        // Arrange
        var divida = new AtualizarDividaRequest{ Valor = 190 };
        var dividaEntity = new DividaEntity{Valor = 190 };

        _dividaRepository.ObterDividaAsync(It.IsAny<int>()).ReturnsForAnyArgs(new DividaEntity());
        _dividaRepository.AtualizarDividaAsync(It.IsAny<DividaEntity>()).ReturnsForAnyArgs(dividaEntity);

        // Act
        var result = await _sut.Handle(divida, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which.Value.Valor.Should().Be(divida.Valor);
        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which.Value.Valor.Should().Be(190);



    }
}

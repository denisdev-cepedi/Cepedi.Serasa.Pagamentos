using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class ObtemDividaResquestHandlerTests
{
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly ILogger<ObtemDividaResquestHandler> _logger = Substitute.For<ILogger<ObtemDividaResquestHandler>>();
    private readonly ObtemDividaResquestHandler _sut;

    public ObtemDividaResquestHandlerTests()
    {
        _sut = new ObtemDividaResquestHandler(_dividaRepository, _logger);
    }

    public class ObtemDividaResquestHandlerTestsFixture
    {
        public Mock<IDividaRepository> MockDividaRepository { get; }
        public Mock<ILogger<ObtemDividaResquestHandler>> MockLogger { get; }

        public ObtemDividaResquestHandlerTestsFixture()
        {
            MockDividaRepository = new Mock<IDividaRepository>();
            MockLogger = new Mock<ILogger<ObtemDividaResquestHandler>>();
        }
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDividaFound()
    {
        // Arrange
        var fixture = new ObtemDividaResquestHandlerTestsFixture();
        var request = new ObtemDividaRequest { Id = 1 };
        var expectedValor = 100.50;
        var expectedDataDeVencimento = new DateTime(2024, 06, 15);
        fixture.MockDividaRepository.Setup(r => r.ObterDividaAsync(request.Id))
            .ReturnsAsync(new DividaEntity { Valor = expectedValor, DataDeVencimento = expectedDataDeVencimento });

        var handler = new ObtemDividaResquestHandler(fixture.MockDividaRepository.Object, fixture.MockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedValor, result.Value.Valor);
        Assert.Equal(expectedDataDeVencimento, result.Value.DataDeVencimento);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDividaNotFound()
    {
        // Arrange
        var fixture = new ObtemDividaResquestHandlerTestsFixture();
        var request = new ObtemDividaRequest { Id = 1 };
        fixture.MockDividaRepository.Setup(r => r.ObterDividaAsync(request.Id))
            .ReturnsAsync((DividaEntity)null);

        var handler = new ObtemDividaResquestHandler(fixture.MockDividaRepository.Object, fixture.MockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<SemResultadosException>(result.Exception);
    }



}

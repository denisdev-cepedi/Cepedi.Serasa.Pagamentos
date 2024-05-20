using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class ObterPagamentoRequestHandlerTest
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPagamentoFound()
    {
        // Arrange
        var fixture = new ObterPagamentoResquestHandlerTestsFixture();
        var request = new ObterPagamentoRequest { Id = 3 };
        var expectedValor = 40;
        var expectedDataDeVencimento = new DateTime(2024, 05, 14);
        fixture.MockPagamentoRepository.Setup(r => r.ObterPagamentoAsync(request.Id))
            .ReturnsAsync(new PagamentoEntity { Valor = expectedValor, DataDeVencimento = expectedDataDeVencimento });

        var handler = new ObterPagamentoRequestHandler(fixture.MockPagamentoRepository.Object, fixture.MockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedValor, result.Value.valor);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPagamentoNotFound()
    {
        // Arrange
        var fixture = new ObterPagamentoResquestHandlerTestsFixture();
        var request = new ObterPagamentoRequest { Id = 1 };
        fixture.MockPagamentoRepository.Setup(r => r.ObterPagamentoAsync(request.Id))
            .ReturnsAsync((PagamentoEntity)null);

        var handler = new ObterPagamentoRequestHandler(fixture.MockPagamentoRepository.Object, fixture.MockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<SemResultadosException>(result.Exception);
    }
}

public class ObterPagamentoResquestHandlerTestsFixture
    {
        public Mock<IPagamentoRepository> MockPagamentoRepository { get; }
        public Mock<ILogger<ObterPagamentoRequestHandler>> MockLogger { get; }

        public ObterPagamentoResquestHandlerTestsFixture()
        {
            MockPagamentoRepository = new Mock<IPagamentoRepository>();
            MockLogger = new Mock<ILogger<ObterPagamentoRequestHandler>>();
        }
    }

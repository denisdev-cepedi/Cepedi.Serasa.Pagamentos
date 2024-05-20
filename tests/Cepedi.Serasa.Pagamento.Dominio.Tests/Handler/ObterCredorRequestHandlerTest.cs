using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class ObterCredorRequestHandlerTest
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDividaFound()
    {
        // Arrange
        var fixture = new ObterCredorResquestHandlerTestsFixture();
        var request = new ObterCredorRequest { Id = 1 };
        var expectedValor = "Paulo";
        var expectedDataDeVencimento = new DateTime(2024, 06, 15);
        fixture.MockCredorRepository.Setup(r => r.ObterCredorAsync(request.Id))
            .ReturnsAsync(new CredorEntity { Nome = expectedValor });

        var handler = new ObterCredorRequestHandler(fixture.MockCredorRepository.Object, fixture.MockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedValor, result.Value.nome);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDividaNotFound()
    {
        // Arrange
        var fixture = new ObterCredorResquestHandlerTestsFixture();
        var request = new ObterCredorRequest { Id = 1 };
        fixture.MockCredorRepository.Setup(r => r.ObterCredorAsync(request.Id))
            .ReturnsAsync((CredorEntity)null);

        var handler = new ObterCredorRequestHandler(fixture.MockCredorRepository.Object, fixture.MockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<SemResultadosException>(result.Exception);
    }
}

 public class ObterCredorResquestHandlerTestsFixture
    {
        public Mock<ICredorRepository> MockCredorRepository { get; }
        public Mock<ILogger<ObterCredorRequestHandler>> MockLogger { get; }

        public ObterCredorResquestHandlerTestsFixture()
        {
            MockCredorRepository = new Mock<ICredorRepository>();
            MockLogger = new Mock<ILogger<ObterCredorRequestHandler>>();
        }
    }

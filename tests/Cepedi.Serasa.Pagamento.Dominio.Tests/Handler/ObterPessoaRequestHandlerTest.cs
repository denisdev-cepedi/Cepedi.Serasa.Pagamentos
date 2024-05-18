using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;

namespace Cepedi.Serasa.Pessoa.Dominio.Tests;

public class ObterPessoaRequestHandlerTest
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<ObterPessoaRequestHandler> _logger = Substitute.For<ILogger<ObterPessoaRequestHandler>>();
    private readonly ObterPessoaRequestHandler _sut;
    public ObterPessoaRequestHandlerTest()
    {
        _sut = new ObterPessoaRequestHandler(_logger, _pessoaRepository);
    }

    public class ObterPessoaResquestHandlerTestsFixture
    {
        public Mock<IPessoaRepository> MockPessoaRepository { get; }
        public Mock<ILogger<ObterPessoaRequestHandler>> MockLogger { get; }

        public ObterPessoaResquestHandlerTestsFixture()
        {
            MockPessoaRepository = new Mock<IPessoaRepository>();
            MockLogger = new Mock<ILogger<ObterPessoaRequestHandler>>();
        }
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPessoaFound()
    {
        // Arrange
        var fixture = new ObterPessoaResquestHandlerTestsFixture();
        var request = new ObterPessoaRequest { Id = 4 };
        var expectedValor = "Teste3";
        // var expectedCpf = "022222222222";
        fixture.MockPessoaRepository.Setup(r => r.ObterPessoaAsync(request.Id))
            .ReturnsAsync(new PessoaEntity { Nome = expectedValor });

        var handler = new ObterPessoaRequestHandler(fixture.MockLogger.Object, fixture.MockPessoaRepository.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedValor, result.Value.Nome);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPessoaNotFound()
    {
        // Arrange
        var fixture = new ObterPessoaResquestHandlerTestsFixture();
        var request = new ObterPessoaRequest { Id = 1 };
        fixture.MockPessoaRepository.Setup(r => r.ObterPessoaAsync(request.Id))
            .ReturnsAsync((PessoaEntity)null);

        var handler = new ObterPessoaRequestHandler(fixture.MockLogger.Object, fixture.MockPessoaRepository.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<SemResultadosException>(result.Exception);
    }
}

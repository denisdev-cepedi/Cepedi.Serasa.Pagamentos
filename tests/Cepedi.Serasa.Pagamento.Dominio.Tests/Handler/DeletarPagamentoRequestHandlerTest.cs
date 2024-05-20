using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class DeletarPagamentoRequestHandlerTest
{
    private readonly Mock<IPagamentoRepository> _mockPagamentoRepository = new Mock<IPagamentoRepository>();
    private readonly Mock<ILogger<DeletarPagamentoRequestHandler>> _mockLogger = new Mock<ILogger<DeletarPagamentoRequestHandler>>();

    [Fact]
    public async Task Should_Delete_Pagamento_Successfully()
    {
        // Arrange
        var request = new DeletarPagamentoRequest { Id = 1 };
        var pagamentoEntity = new PagamentoEntity { Id = request.Id };

        _mockPagamentoRepository.Setup(repo => repo.ObterPagamentoAsync(request.Id))
            .Returns(Task.FromResult(pagamentoEntity));
        _mockPagamentoRepository.Setup(repo => repo.DeletarPagamentoAsync(pagamentoEntity.Id))
            .Returns(Task.FromResult(pagamentoEntity));

        var handler = new DeletarPagamentoRequestHandler(_mockPagamentoRepository.Object, _mockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Id, result.Value.id);

        _mockPagamentoRepository.Verify(repo => repo.ObterPagamentoAsync(request.Id), Times.Once);
        _mockPagamentoRepository.Verify(repo => repo.DeletarPagamentoAsync(pagamentoEntity.Id), Times.Once);
    }

    [Fact]
    public async Task Should_Return_Error_If_Pagamento_Not_Found()
    {
        // Arrange
        var request = new DeletarPagamentoRequest { Id = 111 };

        _mockPagamentoRepository.Setup(repo => repo.ObterPagamentoAsync(request.Id))
            .Returns(Task.FromResult<PagamentoEntity>(null));

        var handler = new DeletarPagamentoRequestHandler(_mockPagamentoRepository.Object, _mockLogger.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess == false);
        Assert.IsType<SemResultadosException>(result.Exception);

        _mockPagamentoRepository.Verify(repo => repo.ObterPagamentoAsync(request.Id), Times.Once);
        _mockPagamentoRepository.Verify(repo => repo.DeletarPagamentoAsync(It.IsAny<int>()), Times.Never); // Deletion not called
    }

}

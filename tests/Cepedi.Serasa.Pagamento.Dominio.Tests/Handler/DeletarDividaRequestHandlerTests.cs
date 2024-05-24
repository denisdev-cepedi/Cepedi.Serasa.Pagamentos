using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class DeletarDividaRequestHandlerTests
{
    private readonly Mock<IDividaRepository> _mockDividaRepository = new Mock<IDividaRepository>();
    private readonly Mock<ILogger<DeletarDividaRequestHandler>> _mockLogger = new Mock<ILogger<DeletarDividaRequestHandler>>();

    private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

    [Fact]
    public async Task Should_Delete_Divida_Successfully()
    {
        // Arrange
        var request = new DeletarDividaRequest { Id = 11 };
        var dividaEntity = new DividaEntity { Id = request.Id };

        _mockDividaRepository.Setup(repo => repo.ObterDividaAsync(request.Id))
            .Returns(Task.FromResult(dividaEntity));
        _mockDividaRepository.Setup(repo => repo.DeletarDividaAsync(dividaEntity.Id))
            .Returns(Task.FromResult(dividaEntity));

        var handler = new DeletarDividaRequestHandler(_mockDividaRepository.Object, _mockLogger.Object, _unitOfWork);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Id, result.Value.id);

        _mockDividaRepository.Verify(repo => repo.ObterDividaAsync(request.Id), Times.Once);
        _mockDividaRepository.Verify(repo => repo.DeletarDividaAsync(dividaEntity.Id), Times.Once);
    }

    [Fact]
    public async Task Should_Return_Error_If_Divida_Not_Found()
    {
        // Arrange
        var request = new DeletarDividaRequest { Id = 11 };

        _mockDividaRepository.Setup(repo => repo.ObterDividaAsync(request.Id))
            .Returns(Task.FromResult<DividaEntity>(null));

        var handler = new DeletarDividaRequestHandler(_mockDividaRepository.Object, _mockLogger.Object, _unitOfWork);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess == false);
        Assert.IsType<SemResultadosException>(result.Exception);

        _mockDividaRepository.Verify(repo => repo.ObterDividaAsync(request.Id), Times.Once);
        _mockDividaRepository.Verify(repo => repo.DeletarDividaAsync(It.IsAny<int>()), Times.Never); // Deletion not called
    }
}


using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cepedi.Serasa.Pessoa.Dominio.Tests;

public class DeletarPessoaRequestHandlerTest
{
    private readonly Mock<IPessoaRepository> _mockPessoaRepository = new Mock<IPessoaRepository>();
    private readonly Mock<ILogger<DeletarPessoaRequestHandler>> _mockLogger = new Mock<ILogger<DeletarPessoaRequestHandler>>();

    [Fact]
    public async Task Should_Delete_Pessoa_Successfully()
    {
        // Arrange
        var request = new DeletarPessoaRequest { Id = 1 };
        var pessoaEntity = new PessoaEntity { Id = request.Id };

        _mockPessoaRepository.Setup(repo => repo.ObterPessoaAsync(request.Id))
            .Returns(Task.FromResult(pessoaEntity));
        _mockPessoaRepository.Setup(repo => repo.DeletarPessoaAsync(pessoaEntity.Id))
            .Returns(Task.FromResult(pessoaEntity));

        var handler = new DeletarPessoaRequestHandler(_mockLogger.Object, _mockPessoaRepository.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Id, result.Value.Id);

        _mockPessoaRepository.Verify(repo => repo.ObterPessoaAsync(request.Id), Times.Once);
        _mockPessoaRepository.Verify(repo => repo.DeletarPessoaAsync(pessoaEntity.Id), Times.Once);
    }

    [Fact]
    public async Task Should_Return_Error_If_Pessoa_Not_Found()
    {
        // Arrange
        var request = new DeletarPessoaRequest { Id = 111 };

        _mockPessoaRepository.Setup(repo => repo.ObterPessoaAsync(request.Id))
            .Returns(Task.FromResult<PessoaEntity>(null));

        var handler = new DeletarPessoaRequestHandler(_mockLogger.Object, _mockPessoaRepository.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess == false);
        Assert.IsType<SemResultadosException>(result.Exception);

        _mockPessoaRepository.Verify(repo => repo.ObterPessoaAsync(request.Id), Times.Once);
        _mockPessoaRepository.Verify(repo => repo.DeletarPessoaAsync(It.IsAny<int>()), Times.Never); // Deletion not called
    }
}

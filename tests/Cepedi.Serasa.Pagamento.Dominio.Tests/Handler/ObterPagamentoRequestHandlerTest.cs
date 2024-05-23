using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
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
    private readonly Mock<IPagamentoRepository> _pagamentoRepositoryMock;
    private readonly Mock<ILogger<ObterPagamentoRequestHandler>> _loggerMock;
    private readonly ObterPagamentoRequestHandler _handler;

    public ObterPagamentoRequestHandlerTest()
    {
        _pagamentoRepositoryMock = new Mock<IPagamentoRepository>();
        _loggerMock = new Mock<ILogger<ObterPagamentoRequestHandler>>();
        _handler = new ObterPagamentoRequestHandler(_pagamentoRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_PagamentoExistente_DeveRetornarSucesso()
    {
        // Arrange
        var pagamentoId = 1;
        var pagamento = new PagamentoEntity { Id = pagamentoId, Valor = 500.0 };
        var request = new ObterPagamentoRequest { Id = pagamentoId };

        _pagamentoRepositoryMock.Setup(repo => repo.ObterPagamentoAsync(pagamentoId))
            .ReturnsAsync(pagamento);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(pagamento.Valor, result.Value.valor);
        _pagamentoRepositoryMock.Verify(repo => repo.ObterPagamentoAsync(pagamentoId), Times.Once);
    }

    [Fact]
    public async Task Handle_PagamentoNaoExistente_DeveRetornarErro()
    {
        // Arrange
        var pagamentoId = 1;
        var request = new ObterPagamentoRequest { Id = pagamentoId };

        _pagamentoRepositoryMock.Setup(repo => repo.ObterPagamentoAsync(pagamentoId))
            .ReturnsAsync((PagamentoEntity)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        var excecaoAplicacao = Assert.IsType<ExcecaoAplicacao>(result.Exception);
        Assert.Equal(PagamentoErros.PagamentoNaoEncontrado, excecaoAplicacao.ResponseErro);
        _pagamentoRepositoryMock.Verify(repo => repo.ObterPagamentoAsync(pagamentoId), Times.Once);
    }
}

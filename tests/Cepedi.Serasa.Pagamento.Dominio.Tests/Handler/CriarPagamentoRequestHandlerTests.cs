using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class CriarPagamentoRequestHandlerTests
{
    private readonly Mock<ICredorRepository> _credorRepositoryMock;
    private readonly Mock<IPagamentoRepository> _pagamentoRepositoryMock;
    private readonly Mock<ILogger<CriarPagamentoRequestHandler>> _loggerMock;
    private readonly CriarPagamentoRequestHandler _handler;

    public CriarPagamentoRequestHandlerTests()
    {
        _credorRepositoryMock = new Mock<ICredorRepository>();
        _pagamentoRepositoryMock = new Mock<IPagamentoRepository>();
        _loggerMock = new Mock<ILogger<CriarPagamentoRequestHandler>>();
        _handler = new CriarPagamentoRequestHandler(_pagamentoRepositoryMock.Object, _credorRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_CredorExistente_DeveCriarPagamentoERetornarSucesso()
    {
        // Arrange
        var credorId = 1;
        var pagamentoId = 100;
        var credor = new CredorEntity { Id = credorId };
        var request = new CriarPagamentoRequest
        {
            IdCredor = credorId,
            Valor = 500.0,
            DataDePagamento = DateTime.Now,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        _credorRepositoryMock.Setup(repo => repo.ObterCredorAsync(credorId))
            .ReturnsAsync(credor);
        _pagamentoRepositoryMock.Setup(repo => repo.CriarPagamentoAsync(It.IsAny<PagamentoEntity>()))
            .ReturnsAsync((PagamentoEntity pagamento) =>
            {
                pagamento.Id = pagamentoId;
                return pagamento;
            });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(pagamentoId, result.Value.idPagamento);
        Assert.Equal(request.Valor, result.Value.valor);
        _credorRepositoryMock.Verify(repo => repo.ObterCredorAsync(credorId), Times.Once);
        _pagamentoRepositoryMock.Verify(repo => repo.CriarPagamentoAsync(It.IsAny<PagamentoEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CredorNaoExistente_DeveRetornarErro()
    {
        // Arrange
        var credorId = 1;
        var request = new CriarPagamentoRequest
        {
            IdCredor = credorId,
            Valor = 500.0,
            DataDePagamento = DateTime.Now,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        _credorRepositoryMock.Setup(repo => repo.ObterCredorAsync(credorId))
            .ReturnsAsync((CredorEntity)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<ExcecaoAplicacao>(result.Exception);
        var excecaoAplicacao = Assert.IsType<ExcecaoAplicacao>(result.Exception);
        Assert.Equal(CredorErrors.CredorInexistente, excecaoAplicacao.ResponseErro);
        _credorRepositoryMock.Verify(repo => repo.ObterCredorAsync(credorId), Times.Once);
        _pagamentoRepositoryMock.Verify(repo => repo.CriarPagamentoAsync(It.IsAny<PagamentoEntity>()), Times.Never);
    }

}

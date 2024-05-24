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
using Cepedi.Serasa.Pagamento.Dados;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;



public class CriarPagamentoRequestHandlerTests
{
    private readonly Mock<IPagamentoRepository> _pagamentoRepositoryMock;
    private readonly Mock<ICredorRepository> _credorRepositoryMock;
    private readonly Mock<IDividaRepository> _dividaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<CriarPagamentoRequestHandler>> _loggerMock;
    private readonly CriarPagamentoRequestHandler _handler;

    public CriarPagamentoRequestHandlerTests()
    {
        _pagamentoRepositoryMock = new Mock<IPagamentoRepository>();
        _credorRepositoryMock = new Mock<ICredorRepository>();
        _dividaRepositoryMock = new Mock<IDividaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<CriarPagamentoRequestHandler>>();
        _handler = new CriarPagamentoRequestHandler(_pagamentoRepositoryMock.Object, _credorRepositoryMock.Object, _dividaRepositoryMock.Object, _loggerMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_DividaNaoEncontrada_RetornaErro()
    {
        // Arrange
        var request = new CriarPagamentoRequest { IdDivida = 1, Valor = 100, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now.AddDays(30) };

        _dividaRepositoryMock.Setup(repo => repo.ObterDividaAsync(request.IdDivida)).ReturnsAsync((DividaEntity)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DividaErros.DividaNaoEncontrada, result.);
    }

    [Fact]
    public async Task Handle_ValorPagamentoIncompativel_RetornaErro()
    {
        // Arrange
        var request = new CriarPagamentoRequest { IdDivida = 1, Valor = 200, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now.AddDays(30) };
        var dividaEntity = new DividaEntity { Id = 1, Valor = 100, IdCredor = 1 };

        _dividaRepositoryMock.Setup(repo => repo.ObterDividaAsync(request.IdDivida)).ReturnsAsync(dividaEntity);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(PagamentoErros.ValorPagamentoIncompativelComDivida, result.Error.Code);
    }

    [Fact]
    public async Task Handle_Sucesso_RetornaPagamentoCriado()
    {
        // Arrange
        var request = new CriarPagamentoRequest { IdDivida = 1, Valor = 100, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now.AddDays(30) };
        var dividaEntity = new DividaEntity { Id = 1, Valor = 100, IdCredor = 1 };
        var credorEntity = new CredorEntity { Id = 1 };

        _dividaRepositoryMock.Setup(repo => repo.ObterDividaAsync(request.IdDivida)).ReturnsAsync(dividaEntity);
        _credorRepositoryMock.Setup(repo => repo.ObterCredorAsync(dividaEntity.IdCredor)).ReturnsAsync(credorEntity);
        _pagamentoRepositoryMock.Setup(repo => repo.QuitarPagamentoAsync(dividaEntity)).ReturnsAsync(dividaEntity);
        _pagamentoRepositoryMock.Setup(repo => repo.CriarPagamentoAsync(It.IsAny<PagamentoEntity>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(request.Valor, result.Value.Valor);
    }
}


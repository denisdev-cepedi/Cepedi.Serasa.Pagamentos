using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class ObterPagamentoRequestHandlerTests
{
    private readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
    private readonly ILogger<ObterPagamentoRequestHandler> _logger = Substitute.For<ILogger<ObterPagamentoRequestHandler>>();
    private readonly ObterPagamentoRequestHandler _sut;

    public ObterPagamentoRequestHandlerTests()
    {
        _sut = new ObterPagamentoRequestHandler(_pagamentoRepository, _logger);
    }

    [Fact]
    public async Task Handle_DeveRetornarPagamentoComSucesso()
    {
        // Arrange
        var request = new ObterPagamentoRequest { Id = 1 };
        var pagamentoEntity = new PagamentoEntity { Id = 1, Valor = 100 };

        _pagamentoRepository.ObterPagamentoAsync(request.Id)
            .Returns(pagamentoEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObterPagamentoResponse>>().Which
            .Value.valor.Should().Be(pagamentoEntity.Valor);
        await _pagamentoRepository.Received(1).ObterPagamentoAsync(request.Id);
    }

    [Fact]
    public async Task Handle_QuandoPagamentoNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var request = new ObterPagamentoRequest { Id = 1 };

        _pagamentoRepository.ObterPagamentoAsync(request.Id)
            .Returns((PagamentoEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObterPagamentoResponse>>().Which
            .Value.Should().BeNull();
    }
}

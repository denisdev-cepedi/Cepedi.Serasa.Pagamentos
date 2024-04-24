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

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class CriarPagamentoRequestHandlerTests
{
    private readonly IPagamentoRepository _pagamentoRepository =
    Substitute.For<IPagamentoRepository>();
    private readonly ILogger<CriarPagamentoRequestHandler> _logger = Substitute.For<ILogger<CriarPagamentoRequestHandler>>();
    private readonly CriarPagamentoRequestHandler _sut;

    public CriarPagamentoRequestHandlerTests()
    {
        _sut = new CriarPagamentoRequestHandler(_pagamentoRepository, _logger);
    }

    [Fact]
    public async Task CriarPagamentoAsync_QuandoCriar_DeveRetornarSucesso()
    {
        //Arrange 
        var pagamento = new CriarPagamentoRequest { Valor = 400 };
        _pagamentoRepository.CriarPagamentoAsync(It.IsAny<PagamentoEntity>())
            .ReturnsForAnyArgs(new PagamentoEntity());

        //Act
        var result = await _sut.Handle(pagamento, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CriarPagamentoResponse>>().Which
            .Value.valor.Should().Be(pagamento.Valor);
        result.Should().BeOfType<Result<CriarPagamentoResponse>>().Which
            .Value.valor.Should().Be(400);
    }

}

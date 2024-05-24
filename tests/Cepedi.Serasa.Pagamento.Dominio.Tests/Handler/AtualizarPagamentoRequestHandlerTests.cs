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
using Cepedi.Serasa.Pagamento.Dados;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class AtualizarPagamentoRequestHandlerTests
{
    private readonly IPagamentoRepository _pagamentoRepository =
    Substitute.For<IPagamentoRepository>();
    private readonly ILogger<AtualizarPagamentoRequestHandler> _logger = Substitute.For<ILogger<AtualizarPagamentoRequestHandler>>();
    private readonly AtualizarPagamentoRequestHandler _sut;

    private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

    public AtualizarPagamentoRequestHandlerTests()
    {
        _sut = new AtualizarPagamentoRequestHandler(_pagamentoRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task AtualizarPagamentoAsync_QuandoAtualizar_DeveRetornarSucesso()
    {
        //Arrange 
        var pagamento = new AtualizarPagamentoRequest { Valor = 300 };
        var pagamentoEntity = new PagamentoEntity { Valor = 300 };
        _pagamentoRepository.ObterPagamentoAsync(It.IsAny<int>()).ReturnsForAnyArgs(new PagamentoEntity());
        _pagamentoRepository.AtualizarPagamentoAsync(It.IsAny<PagamentoEntity>())
            .ReturnsForAnyArgs(pagamentoEntity);

        //Act
        var result = await _sut.Handle(pagamento, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<AtualizarPagamentoResponse>>().Which
            .Value.valor.Should().Be(pagamento.Valor);

        result.Should().BeOfType<Result<AtualizarPagamentoResponse>>().Which
            .Value.valor.Should().Be(pagamentoEntity.Valor);
    }
}

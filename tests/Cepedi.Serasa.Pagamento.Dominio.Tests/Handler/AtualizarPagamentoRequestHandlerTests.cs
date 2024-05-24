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
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Dominio;

public class AtualizarPagamentoRequestHandlerTests
{
    private readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<AtualizarPagamentoRequestHandler> _logger = Substitute.For<ILogger<AtualizarPagamentoRequestHandler>>();
    private readonly AtualizarPagamentoRequestHandler _sut;

    public AtualizarPagamentoRequestHandlerTests()
    {
        _sut = new AtualizarPagamentoRequestHandler(_pagamentoRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_QuandoPagamentoExistente_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarPagamentoRequest { Id = 1, Valor = 100 };
        var pagamentoEntity = new PagamentoEntity { Id = 1, Valor = 50 };

        _pagamentoRepository.ObterPagamentoAsync(request.Id)
            .Returns(pagamentoEntity);

        _pagamentoRepository.AtualizarPagamentoAsync(Arg.Any<PagamentoEntity>())
            .Returns(pagamentoEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarPagamentoResponse>>().Which
            .Value.valor.Should().Be(request.Valor);
        await _pagamentoRepository.Received(1).AtualizarPagamentoAsync(pagamentoEntity);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoPagamentoInexistente_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarPagamentoRequest { Id = 1, Valor = 100 };

        _pagamentoRepository.ObterPagamentoAsync(request.Id)
            .Returns((PagamentoEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarPagamentoResponse>>().Which
            .Value.Should().BeNull();
    }
}

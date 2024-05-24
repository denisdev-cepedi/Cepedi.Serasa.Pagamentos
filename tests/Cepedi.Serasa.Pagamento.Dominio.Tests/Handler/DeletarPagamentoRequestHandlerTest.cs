using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class DeletarPagamentoRequestHandlerTests
{
    private readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<DeletarPagamentoRequestHandler> _logger = Substitute.For<ILogger<DeletarPagamentoRequestHandler>>();
    private readonly DeletarPagamentoRequestHandler _sut;

    public DeletarPagamentoRequestHandlerTests()
    {
        _sut = new DeletarPagamentoRequestHandler(_pagamentoRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveDeletarPagamentoComSucesso()
    {
        // Arrange
        var request = new DeletarPagamentoRequest { Id = 1 };

        var pagamentoEntity = new PagamentoEntity { Id = 1 };

        _pagamentoRepository.ObterPagamentoAsync(request.Id)
            .Returns(pagamentoEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarPagamentoResponse>>().Which
            .Value.id.Should().Be(request.Id);
        await _pagamentoRepository.Received(1).ObterPagamentoAsync(request.Id);
        await _pagamentoRepository.Received(1).DeletarPagamentoAsync(request.Id);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoPagamentoNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var request = new DeletarPagamentoRequest { Id = 1 };

        _pagamentoRepository.ObterPagamentoAsync(request.Id)
            .Returns((PagamentoEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarPagamentoResponse>>().Which
            .Value.Should().BeNull();
    }
}

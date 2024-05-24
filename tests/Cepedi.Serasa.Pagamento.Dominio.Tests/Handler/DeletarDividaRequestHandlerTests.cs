using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
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
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<DeletarDividaRequestHandler> _logger = Substitute.For<ILogger<DeletarDividaRequestHandler>>();
    private readonly DeletarDividaRequestHandler _sut;

    public DeletarDividaRequestHandlerTests()
    {
        _sut = new DeletarDividaRequestHandler(_dividaRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveDeletarDividaComSucesso()
    {
        // Arrange
        var request = new DeletarDividaRequest { Id = 1 };

        var dividaEntity = new DividaEntity { Id = 1 };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns(dividaEntity);
        _dividaRepository.DeletarDividaAsync(Arg.Any<int>())
            .Returns(dividaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarDividaResponse>>().Which
            .Value.id.Should().Be(request.Id);
        await _dividaRepository.Received(1).ObterDividaAsync(request.Id);
        await _dividaRepository.Received(1).DeletarDividaAsync(request.Id);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoDividaNaoEncontrada_DeveRetornarErro()
    {
        // Arrange
        var request = new DeletarDividaRequest { Id = 1 };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns((DividaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarDividaResponse>>().Which
            .Value.Should().BeNull();
    }

    [Fact]
    public async Task Handle_QuandoErroAoDeletarDivida_DeveRetornarErro()
    {
        // Arrange
        var request = new DeletarDividaRequest { Id = 1 };

        var dividaEntity = new DividaEntity { Id = 1 };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns(dividaEntity);
        _dividaRepository.DeletarDividaAsync(Arg.Any<int>())
            .Returns((DividaEntity)null); // Simula erro ao deletar divida

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarDividaResponse>>().Which
            .Value.Should().BeNull();
    }
}


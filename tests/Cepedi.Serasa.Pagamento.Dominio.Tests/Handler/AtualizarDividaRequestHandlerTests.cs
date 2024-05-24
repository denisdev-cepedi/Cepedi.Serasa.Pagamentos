using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class AtualizarDividaRequestHandlerTests
{
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<AtualizarDividaRequestHandler> _logger = Substitute.For<ILogger<AtualizarDividaRequestHandler>>();
    private readonly AtualizarDividaRequestHandler _sut;

    public AtualizarDividaRequestHandlerTests()
    {
        _sut = new AtualizarDividaRequestHandler(_dividaRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_QuandoDividaExistente_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarDividaRequest { Id = 1, Valor = 1000, DataDeVencimento = DateTime.Now.AddDays(30) };
        var dividaEntity = new DividaEntity { Id = 1, Valor = 500, DataDeVencimento = DateTime.Now.AddDays(60) };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns(dividaEntity);

        _dividaRepository.AtualizarDividaAsync(Arg.Any<DividaEntity>())
            .Returns(dividaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which
            .Value.Valor.Should().Be(request.Valor);
        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which
            .Value.DataDeVencimento.Should().Be(request.DataDeVencimento);
        await _dividaRepository.Received(1).AtualizarDividaAsync(dividaEntity);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoDividaInexistente_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarDividaRequest { Id = 1, Valor = 1000, DataDeVencimento = DateTime.Now.AddDays(30) };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns((DividaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which
            .Value.Should().BeNull();
    }

    [Fact]
    public async Task Handle_QuandoErroAtualizarDivida_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarDividaRequest { Id = 1, Valor = 1000, DataDeVencimento = DateTime.Now.AddDays(30) };
        var dividaEntity = new DividaEntity { Id = 1, Valor = 500, DataDeVencimento = DateTime.Now.AddDays(60) };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns(dividaEntity);

        _dividaRepository.AtualizarDividaAsync(Arg.Any<DividaEntity>())
            .Returns((DividaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which
            .Value.Should().BeNull();
    }
}

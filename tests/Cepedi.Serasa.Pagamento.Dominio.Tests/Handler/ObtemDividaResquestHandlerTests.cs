using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class ObtemDividaResquestHandlerTests
{
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly ILogger<ObtemDividaResquestHandler> _logger = Substitute.For<ILogger<ObtemDividaResquestHandler>>();
    private readonly ObtemDividaResquestHandler _sut;

    public ObtemDividaResquestHandlerTests()
    {
        _sut = new ObtemDividaResquestHandler(_dividaRepository, _logger);
    }

    [Fact]
    public async Task Handle_DeveRetornarDividaComSucesso()
    {
        // Arrange
        var request = new ObtemDividaRequest { Id = 1 };
        var dividaEntity = new DividaEntity { Id = 1, Valor = 100, DataDeVencimento = DateTime.Now.AddDays(30) };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns(dividaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObtemDividaResponse>>().Which
            .Value.Valor.Should().Be(dividaEntity.Valor);
        result.Should().BeOfType<Result<ObtemDividaResponse>>().Which
            .Value.DataDeVencimento.Should().Be(dividaEntity.DataDeVencimento);
        await _dividaRepository.Received(1).ObterDividaAsync(request.Id);
    }

    [Fact]
    public async Task Handle_QuandoDividaNaoEncontrada_DeveRetornarErro()
    {
        // Arrange
        var request = new ObtemDividaRequest { Id = 1 };

        _dividaRepository.ObterDividaAsync(request.Id)
            .Returns((DividaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObtemDividaResponse>>().Which
            .Value.Should().BeNull();
    }
}

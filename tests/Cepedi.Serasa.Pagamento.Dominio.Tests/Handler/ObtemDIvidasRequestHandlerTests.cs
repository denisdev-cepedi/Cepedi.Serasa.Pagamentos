using Cepedi.Serasa.Pagamento.Compartilhado;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class ObtemDividasRequestHandlerTests
{
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly ILogger<ObtemDividasRequestHandler> _logger = Substitute.For<ILogger<ObtemDividasRequestHandler>>();
    private readonly ObtemDividasRequestHandler _sut;

    public ObtemDividasRequestHandlerTests()
    {
        _sut = new ObtemDividasRequestHandler(_dividaRepository, _logger);
    }

    [Fact]
    public async Task Handle_DeveRetornarDividasComSucesso()
    {
        // Arrange
        var request = new ObtemDividasRequest { Id = 1 };
        var dividaEntities = new List<DividaEntity>
        {
            new DividaEntity { Id = 1, Valor = 100, DataDeVencimento = DateTime.Today },
            new DividaEntity { Id = 2, Valor = 200, DataDeVencimento = DateTime.Today.AddDays(1) }
        };

        _dividaRepository.ObterDividasPessoaAsync(request.Id)
            .Returns(dividaEntities);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObtemDividasResponse>>().Which
            .Value.Dividas.Should().NotBeNull()
            .And.HaveCount(2)
            .And.ContainItemsAssignableTo<ObtemDividaResponse>()
            .And.Contain(x => x.Valor == 100 && x.DataDeVencimento == DateTime.Today)
            .And.Contain(x => x.Valor == 200 && x.DataDeVencimento == DateTime.Today.AddDays(1));
        await _dividaRepository.Received(1).ObterDividasPessoaAsync(request.Id);
    }

    [Fact]
    public async Task Handle_QuandoDividasNaoEncontradas_DeveRetornarErro()
    {
        // Arrange
        var request = new ObtemDividasRequest { Id = 1 };

        _dividaRepository.ObterDividasPessoaAsync(request.Id)
            .Returns((List<DividaEntity>)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObtemDividasResponse>>().Which
            .Value.Should().BeNull();
    }
}

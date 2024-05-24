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
    private readonly ILogger<AtualizarDividaRequestHandler> _logger = Substitute.For<ILogger<AtualizarDividaRequestHandler>>();
    private readonly AtualizarDividaRequestHandler _sut;

    private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

    public AtualizarDividaRequestHandlerTests()
    {
        _sut = new AtualizarDividaRequestHandler(_dividaRepository, _logger, _unitOfWork);
    }
    [Fact]
    public async Task AtualizarDivida_DeveRetornarSucesso()
    {
        var divida = new AtualizarDividaRequest { Id = 1, Valor = 100 };
        var dividaEntity = new DividaEntity { Id = 1, Valor = 100 };
        _dividaRepository.ObterDividaAsync(It.IsAny<int>()).ReturnsForAnyArgs(new DividaEntity());
        _dividaRepository.AtualizarDividaAsync(It.IsAny<DividaEntity>()).ReturnsForAnyArgs(dividaEntity);

        var result = await _sut.Handle(divida, CancellationToken.None);
        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which
            .Value.Valor.Should().Be(divida.Valor);

        result.Should().BeOfType<Result<AtualizarDividaResponse>>().Which
            .Value.Valor.Should().Be(dividaEntity.Valor);
    }
}

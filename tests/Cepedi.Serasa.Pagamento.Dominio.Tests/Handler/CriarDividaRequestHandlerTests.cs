using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class CriarDividaRequestHandlerTests
{
    private readonly IDividaRepository _dividaRepository =
    Substitute.For<IDividaRepository>();
    private readonly ICredorRepository _credorRepository =
    Substitute.For<ICredorRepository>();
    private readonly IPessoaRepository _pessoaRepository =
    Substitute.For<IPessoaRepository>();
    private readonly ILogger<CriarDividaRequestHandler> _logger = Substitute.For<ILogger<CriarDividaRequestHandler>>();
    private readonly CriarDividaRequestHandler _sut;

    private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

    public CriarDividaRequestHandlerTests()
    {
        _sut = new CriarDividaRequestHandler(_logger, _dividaRepository, _credorRepository, _pessoaRepository, _unitOfWork);
    }

    [Fact]
    public async Task CriarDividaAsync_QuandoCriar_DeveRetornarSucesso()
    {
        //Arrange
        var divida = new CriarDividaRequest
        {
            Valor = 100.00,
            DataDeVencimento = DateTime.Now,
            IdPessoa = 1,
            IdCredor = 2
        };
        _dividaRepository.CriarDividaAsync(It.IsAny<DividaEntity>())
            .ReturnsForAnyArgs(new DividaEntity());

        //Act
        var result = await _sut.Handle(divida, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CriarDividaResponse>>().Which
            .Value.Valor.Should().Be(divida.Valor);
        result.Should().BeOfType<Result<CriarDividaResponse>>().Which
            .Value.Valor.Should().Be(divida.Valor);
    }
}

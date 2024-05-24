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

namespace Cepedi.Serasa.Credor.Dominio.Tests;

public class CriarCredorRequestHandlerTest
{
    private readonly ICredorRepository _credorRepository =
    Substitute.For<ICredorRepository>();
    private readonly ILogger<CriarCredorRequestHandler> _logger = Substitute.For<ILogger<CriarCredorRequestHandler>>();
    private readonly CriarCredorRequestHandler _sut;

    private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

    public CriarCredorRequestHandlerTest()
    {
        _sut = new CriarCredorRequestHandler(_credorRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task CriarCredorAsync_QuandoCriar_DeveRetornarSucesso()
    {
        //Arrange
        var credor = new CriarCredorRequest { Nome = "Dennis" };
        _credorRepository.CriarCredorAsync(It.IsAny<CredorEntity>())
            .ReturnsForAnyArgs(new CredorEntity());

        //Act
        var result = await _sut.Handle(credor, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CriarCredorResponse>>().Which
            .Value.nome.Should().Be(credor.Nome);
        result.Should().BeOfType<Result<CriarCredorResponse>>().Which
            .Value.nome.Should().Be(credor.Nome);
    }
}

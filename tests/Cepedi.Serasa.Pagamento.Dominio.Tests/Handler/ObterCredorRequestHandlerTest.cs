using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class ObterCredorRequestHandlerTests
{
    private readonly ICredorRepository _credorRepository = Substitute.For<ICredorRepository>();
    private readonly ILogger<ObterCredorRequestHandler> _logger = Substitute.For<ILogger<ObterCredorRequestHandler>>();
    private readonly ObterCredorRequestHandler _sut;

    public ObterCredorRequestHandlerTests()
    {
        _sut = new ObterCredorRequestHandler(_credorRepository, _logger);
    }

    [Fact]
    public async Task Handle_DeveRetornarCredorComSucesso()
    {
        // Arrange
        var request = new ObterCredorRequest { Id = 1 };
        var credorEntity = new CredorEntity { Id = 1, Nome = "Nome do Credor" };

        _credorRepository.ObterCredorAsync(request.Id)
            .Returns(credorEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObterCredorResponse>>().Which
            .Value.nome.Should().Be(credorEntity.Nome);
        await _credorRepository.Received(1).ObterCredorAsync(request.Id);
    }

    [Fact]
    public async Task Handle_QuandoCredorNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var request = new ObterCredorRequest { Id = 1 };

        _credorRepository.ObterCredorAsync(request.Id)
            .Returns((CredorEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObterCredorResponse>>().Which
            .Value.Should().BeNull();
    }
}

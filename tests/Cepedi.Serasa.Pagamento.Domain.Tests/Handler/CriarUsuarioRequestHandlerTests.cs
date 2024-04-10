using Cepedi.Serasa.Pagamento.Domain.Entities;
using Cepedi.Serasa.Pagamento.Domain.Handlers;
using Cepedi.Serasa.Pagamento.Domain.Repository;
using Cepedi.Shareable.Requests;
using Cepedi.Shareable.Responses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Domain.Tests;

public class CriarUsuarioRequestHandlerTests
{
    private readonly IUsuarioRepository _usuarioRepository =
    Substitute.For<IUsuarioRepository>();
    private readonly ILogger<CriarUsuarioRequestHandler> _logger = Substitute.For<ILogger<CriarUsuarioRequestHandler>>();
    private readonly CriarUsuarioRequestHandler _sut;

    public CriarUsuarioRequestHandlerTests()
    {
        _sut = new CriarUsuarioRequestHandler(_usuarioRepository, _logger);
    }

    [Fact]
    public async Task CriarUsuarioAsync_QuandoCriar_DeveRetornarSucesso()
    {
        //Arrange 
        var usuario = new CriarUsuarioRequest { Nome= "Denis" };
        _usuarioRepository.CriarUsuarioAsync(It.IsAny<UsuarioEntity>())
            .ReturnsForAnyArgs(new UsuarioEntity());

        //Act
        var result = await _sut.Handle(usuario, CancellationToken.None);

        //Assert 
        result.Should().BeOfType<Result<CriarUsuarioResponse>>().Which
            .Value.nome.Should().Be(usuario.Nome);
        result.Should().BeOfType<Result<CriarUsuarioResponse>>().Which
            .Value.nome.Should().NotBeEmpty();
    }

}

// using Cepedi.Serasa.Pagamento.Dominio.Entidades;
// using Cepedi.Serasa.Pagamento.Dominio.Handlers;
// using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
// using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
// using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
// using FluentAssertions;
// using Microsoft.Extensions.Logging;
// using Moq;
// using NSubstitute;
// using OperationResult;

// namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

// public class AtualizarUsuarioRequestHandlerTests
// {
//     private readonly IUsuarioRepository _usuarioRepository =
//     Substitute.For<IUsuarioRepository>();
//     private readonly ILogger<AtualizarUsuarioRequestHandler> _logger = Substitute.For<ILogger<AtualizarUsuarioRequestHandler>>();
//     private readonly AtualizarUsuarioRequestHandler _sut;

//     public AtualizarUsuarioRequestHandlerTests()
//     {
//         _sut = new AtualizarUsuarioRequestHandler(_usuarioRepository, _logger);
//     }

//     [Fact]
//     public async Task AtualizarUsuarioAsync_QuandoAtualizar_DeveRetornarSucesso()
//     {
//         //Arrange 
//         var usuario = new AtualizarUsuarioRequest { Nome= "Denis" };
//         var usuarioEntity = new UsuarioEntity { Nome = "Denis" };
//         _usuarioRepository.ObterUsuarioAsync(It.IsAny<int>()).ReturnsForAnyArgs(new UsuarioEntity());
//         _usuarioRepository.AtualizarUsuarioAsync(It.IsAny<UsuarioEntity>())
//             .ReturnsForAnyArgs(usuarioEntity);

//         //Act
//         var result = await _sut.Handle(usuario, CancellationToken.None);

//         //Assert 
//         result.Should().BeOfType<Result<AtualizarUsuarioResponse>>().Which
//             .Value.nome.Should().Be(usuario.Nome);

//         result.Should().BeOfType<Result<AtualizarUsuarioResponse>>().Which
//             .Value.nome.Should().NotBeEmpty();
//     }

// }

// using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
// using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
// using Cepedi.Serasa.Pagamento.Dominio.Entidades;
// using Cepedi.Serasa.Pagamento.Dominio.Handlers;
// using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
// using FluentAssertions;
// using Microsoft.Extensions.Logging;
// using Moq;
// using NSubstitute;
// using OperationResult;

// namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

// public class CriarCredorRequestHandlerTests
// {
//     private readonly ICredorRepository _CredorRepository = Substitute.For<ICredorRepository>();
//     private readonly ILogger<CriarCredorRequestHandler> _logger = Substitute.For<ILogger<CriarCredorRequestHandler>>();
//     private readonly CriarCredorRequestHandler _sut;

//     public CriarCredorRequestHandlerTests()
//     {
//         _sut = new CriarCredorRequestHandler(_logger, _CredorRepository);
//     }

//     [Fact]
//     public async Task CriarCredorAsync_QuandoCriar_DeveRetornarSucesso()
//     {
//         var Credor = new CriarCredorRequest { Nome = "Wilton" };
//         _CredorRepository.CriarCredorAsync(It.IsAny<CredorEntity>()).ReturnsForAnyArgs(new CredorEntity());
//         var result = await _sut.Handle(Credor, CancellationToken.None);

//         result.Should().BeOfType<Result<CriarCredorResponse>>().Which.Value.Nome.Should().Be(Credor.Nome);
//         result.Should().BeOfType<Result<CriarCredorResponse>>().Which.Value.Nome.Should().NotBeEmpty();
//     }
// }

// using Cepedi.Serasa.Pagamento.Compartilhado;
// using Cepedi.Serasa.Pagamento.Dominio.Entidades;
// using FluentAssertions;
// using Microsoft.Extensions.Logging;
// using Moq;
// using NSubstitute;
// using OperationResult;

// namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

// public class DeletarDividaRequestHandlerTests
// {
//     private readonly IDividaRepository _dividaRepository =
//    Substitute.For<IDividaRepository>();
//     private readonly ILogger<DeletarDividaRequestHandler> _logger = Substitute.For<ILogger<DeletarDividaRequestHandler>>();
//     private readonly DeletarDividaRequestHandler _sut;

//     public DeletarDividaRequestHandlerTests()
//     {
//         _sut = new DeletarDividaRequestHandler(_dividaRepository, _logger);
//     }

//     [Fact]
//     public async Task DeletarDividaAsync_QuandoDeletar_DeveRetornarSucesso()
//     {
//         //Arrange
//         var divida = new DeletarDividaRequest
//         {
//             Id = 1
//         };
//         _dividaRepository.DeletarDividaAsync(It.IsAny<int>())
//             .ReturnsForAnyArgs(new DividaEntity());

//         //Act
//         var result = await _sut.Handle(divida, CancellationToken.None);

//         //Assert 
//         result.Should().BeOfType<Result<DeletarDividaResponse>>().Which
//             .Value.id.Should().Be(divida.Id);
//         result.Should().BeOfType<Result<DeletarDividaResponse>>().Which
//             .Value.id.Should().Be(divida.Id);
//     }
// }


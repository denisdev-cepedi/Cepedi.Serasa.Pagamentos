// using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
// using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
// using Cepedi.Serasa.Pagamento.Dominio;
// using Cepedi.Serasa.Pagamento.Dominio.Entidades;
// using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
// using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
// using Microsoft.Extensions.Logging;
// using Moq;
// using NSubstitute;

// namespace Cepedi.Serasa.Pessoa.Dominio.Tests;

// public class ObterPessoaRequestHandlerTest
// {

//     [Fact]
//     public async Task Handle_ShouldReturnSuccess_WhenPessoaFound()
//     {
//         // Arrange
//         var fixture = new ObterPessoaResquestHandlerTestsFixture();
//         var request = new ObterPessoaRequest { Id = 4 };
//         var expectedValor = "Teste3";
//         // var expectedCpf = "022222222222";
//         fixture.MockPessoaQueryRepository.Setup(r => r.ObterPessoasDapperAsync(request.Id))
//             .ReturnsAsync(new PessoaEntity { Nome = expectedValor });


//         var handler = new ObterPessoaRequestHandler(fixture.MockLogger.Object, fixture.MockPessoaQueryRepository.Object, fixture.MockUnitOfWworkb.Object);

//         // Act
//         var result = await handler.Handle(request, CancellationToken.None);

//         // Assert
//         Assert.True(result.IsSuccess);
//         Assert.NotNull(result.Value);
//         Assert.Equal(expectedValor, result.Value.Nome);
//     }

//     [Fact]
//     public async Task Handle_ShouldReturnError_WhenPessoaNotFound()
//     {
//         // Arrange
//         var fixture = new ObterPessoaResquestHandlerTestsFixture();
//         var request = new ObterPessoaRequest { Id = 1 };
//         fixture.MockPessoaQueryRepository.Setup(r => r.ObterPessoasDapperAsync(request.Id))
//             .ReturnsAsync((PessoaEntity)null);

//         var handler = new ObterPessoaRequestHandler(fixture.MockLogger.Object, fixture.MockPessoaQueryRepository.Object, fixture.MockUnitOfWworkb.Object);

//         // Act
//         var result = await handler.Handle(request, CancellationToken.None);

//         // Assert
//         Assert.False(result.IsSuccess);
//         Assert.IsType<SemResultadosException>(result.Exception);
//     }
// }

// public class ObterPessoaResquestHandlerTestsFixture
// {
//     public Mock<IUnitOfWork> MockUnitOfWworkb { get; }
//     public Mock<IPessoaQueryRepository> MockPessoaQueryRepository { get; }
//     public Mock<ILogger<ObterPessoaRequestHandler>> MockLogger { get; }

//     public ObterPessoaResquestHandlerTestsFixture()
//     {
//         MockPessoaQueryRepository = new Mock<IPessoaQueryRepository>();
//         MockLogger = new Mock<ILogger<ObterPessoaRequestHandler>>();
//         MockUnitOfWworkb = new Mock<IUnitOfWork>();

//     }
// }

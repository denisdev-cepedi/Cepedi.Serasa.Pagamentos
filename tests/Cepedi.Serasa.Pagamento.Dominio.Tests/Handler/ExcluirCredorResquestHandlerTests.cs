// using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
// using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
// using Cepedi.Serasa.Pagamento.Dominio.Entidades;
// using Cepedi.Serasa.Pagamento.Dominio.Handlers;
// using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
// using Microsoft.Extensions.Logging;
// using Moq;

// namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

// public class ExcluirCredorResquestHandlerTests
// {
//     private readonly Mock<ICredorRepository> _mockCredorRepository = new Mock<ICredorRepository>();
//     private readonly Mock<ILogger<ExcluirCredorRequestHandler>> _mockLogger = new Mock<ILogger<ExcluirCredorRequestHandler>>();

//     [Fact]
//     public async Task Should_Delete_Credor_Successfully()
//     {
//         // Arrange
//         var request = new ExcluirCredorRequest { Id = 1 };
//         var credorEntity = new CredorEntity { Id = request.Id };

//         _mockCredorRepository.Setup(repo => repo.ObterCredorAsync(request.Id))
//             .Returns(Task.FromResult(credorEntity));
//         _mockCredorRepository.Setup(repo => repo.ExcluirCredorAsync(credorEntity.Id))
//             .Returns(Task.FromResult(credorEntity));

//         var handler = new ExcluirCredorRequestHandler(_mockCredorRepository.Object, _mockLogger.Object);

//         // Act
//         var result = await handler.Handle(request, CancellationToken.None);

//         // Assert
//         Assert.True(result.IsSuccess);
//         Assert.Equal(request.Id, result.Value.Id);

//         _mockCredorRepository.Verify(repo => repo.ObterCredorAsync(request.Id), Times.Once);
//         _mockCredorRepository.Verify(repo => repo.ExcluirCredorAsync(credorEntity.Id), Times.Once);
//     }

//     [Fact]
//     public async Task Should_Return_Error_If_Credor_Not_Found()
//     {
//         // Arrange
//         var request = new ExcluirCredorRequest { Id = 111 };

//         _mockCredorRepository.Setup(repo => repo.ObterCredorAsync(request.Id))
//             .Returns(Task.FromResult<CredorEntity>(null));

//         var handler = new ExcluirCredorRequestHandler(_mockCredorRepository.Object, _mockLogger.Object);

//         // Act
//         var result = await handler.Handle(request, CancellationToken.None);

//         // Assert
//         Assert.True(result.IsSuccess == false);
//         Assert.IsType<SemResultadosException>(result.Exception);

//         _mockCredorRepository.Verify(repo => repo.ObterCredorAsync(request.Id), Times.Once);
//         _mockCredorRepository.Verify(repo => repo.ExcluirCredorAsync(It.IsAny<int>()), Times.Never); // Deletion not called
//     }
// }

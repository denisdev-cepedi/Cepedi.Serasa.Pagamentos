// using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
// using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
// using Cepedi.Serasa.Pagamento.Dados;
// using Cepedi.Serasa.Pagamento.Dominio.Entidades;
// using Cepedi.Serasa.Pagamento.Dominio.Handlers;
// using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
// using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
// using FluentAssertions;
// using Microsoft.Extensions.Logging;
// using Moq;
// using NSubstitute;
// using OperationResult;

// namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

// public class AtualizarPessoaRequestHandlerTests
// {
//     private readonly IPessoaQueryRepository _pessoaQueryRepository = Substitute.For<IPessoaQueryRepository>();
//     private readonly ILogger<AtualizarPessoaRequestHandler> _logger = Substitute.For<ILogger<AtualizarPessoaRequestHandler>>();
//     private readonly AtualizarPessoaRequestHandler _sut;

//     private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

//     public AtualizarPessoaRequestHandlerTests()
//     {
//         _sut = new AtualizarPessoaRequestHandler(_pessoaQueryRepository, _unitOfWork, _logger);
//     }
//     [Fact]
//     public async Task AtualizarPessoa_DeveRetornarSucesso()
//     {
//         var pessoa = new AtualizarPessoaRequest { Id = 1, Nome = "Pericles" };
//         var pessoaEntity = new PessoaEntity { Id = 1, Nome = "Pericles" };
//         _pessoaQueryRepository.ObterPessoasDapperAsync(It.IsAny<int>()).ReturnsForAnyArgs(new PessoaEntity());
//         _pessoaQueryRepository.AtualizarPessoaDapperAsync(It.IsAny<PessoaEntity>()).ReturnsForAnyArgs(pessoaEntity);

//         var result = await _sut.Handle(pessoa, CancellationToken.None);
//         result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which
//             .Value.Nome.Should().Be(pessoa.Nome);

//         result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which
//             .Value.Nome.Should().Be(pessoaEntity.Nome);
//     }
// }

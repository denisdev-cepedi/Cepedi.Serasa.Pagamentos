using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class AtualizarPessoaRequestHandlerTests
{
    private readonly IPessoaQueryRepository _pessoaQueryRepository = Substitute.For<IPessoaQueryRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<AtualizarPessoaRequestHandler> _logger = Substitute.For<ILogger<AtualizarPessoaRequestHandler>>();
    private readonly AtualizarPessoaRequestHandler _sut;

    public AtualizarPessoaRequestHandlerTests()
    {
        _sut = new AtualizarPessoaRequestHandler(_pessoaQueryRepository, _unitOfWork, _logger);
    }

    [Fact]
    public async Task Handle_QuandoPessoaExistente_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarPessoaRequest { Id = 1, Nome = "Novo Nome" };
        var pessoaEntity = new PessoaEntity { Id = 1, Nome = "Nome Antigo" };

        _pessoaQueryRepository.ObterPessoasDapperAsync(request.Id)
            .Returns(pessoaEntity);

        _pessoaQueryRepository.AtualizarPessoaDapperAsync(Arg.Any<PessoaEntity>())
            .Returns(pessoaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which
            .Value.Nome.Should().Be(request.Nome);
        await _pessoaQueryRepository.Received(1).AtualizarPessoaDapperAsync(pessoaEntity);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoPessoaInexistente_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarPessoaRequest { Id = 1, Nome = "Novo Nome" };

        _pessoaQueryRepository.ObterPessoasDapperAsync(request.Id)
            .Returns((PessoaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which
            .Value.Should().BeNull();
    }
}

using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio.Queries;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pessoa.Dominio.Tests;

public class ObterPessoaRequestHandlerTests
{
    private readonly IPessoaQueryRepository _pessoaQueryRepository = Substitute.For<IPessoaQueryRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<ObterPessoaRequestHandler> _logger = Substitute.For<ILogger<ObterPessoaRequestHandler>>();
    private readonly ObterPessoaRequestHandler _sut;

    public ObterPessoaRequestHandlerTests()
    {
        _sut = new ObterPessoaRequestHandler(_logger, _pessoaQueryRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveRetornarPessoaComSucesso()
    {
        // Arrange
        var request = new ObterPessoaRequest { Id = 1 };
        var pessoaEntity = new PessoaEntity { Id = 1, Nome = "Nome da Pessoa" };

        _pessoaQueryRepository.ObterPessoasDapperAsync(request.Id)
            .Returns(pessoaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObterPessoaResponse>>().Which
            .Value.Nome.Should().Be(pessoaEntity.Nome);
        await _pessoaQueryRepository.Received(1).ObterPessoasDapperAsync(request.Id);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoPessoaNaoEncontrada_DeveRetornarErro()
    {
        // Arrange
        var request = new ObterPessoaRequest { Id = 1 };

        _pessoaQueryRepository.ObterPessoasDapperAsync(request.Id)
            .Returns((PessoaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<ObterPessoaResponse>>().Which
            .Value.Should().BeNull();
        await _unitOfWork.DidNotReceive().SaveChangesAsync(CancellationToken.None);
    }
}

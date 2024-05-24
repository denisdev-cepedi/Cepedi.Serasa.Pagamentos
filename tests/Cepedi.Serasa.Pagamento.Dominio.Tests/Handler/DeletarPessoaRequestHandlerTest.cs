using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pessoa.Dominio.Tests;

public class DeletarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<DeletarPessoaRequestHandler> _logger = Substitute.For<ILogger<DeletarPessoaRequestHandler>>();
    private readonly DeletarPessoaRequestHandler _sut;

    public DeletarPessoaRequestHandlerTests()
    {
        _sut = new DeletarPessoaRequestHandler(_logger, _pessoaRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveDeletarPessoaComSucesso()
    {
        // Arrange
        var request = new DeletarPessoaRequest { Id = 1 };

        var pessoaEntity = new PessoaEntity { Id = 1 };

        _pessoaRepository.ObterPessoaAsync(request.Id)
            .Returns(pessoaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarPessoaResponse>>().Which
            .Value.Id.Should().Be(request.Id);
        await _pessoaRepository.Received(1).ObterPessoaAsync(request.Id);
        await _pessoaRepository.Received(1).DeletarPessoaAsync(request.Id);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoPessoaNaoEncontrada_DeveRetornarErro()
    {
        // Arrange
        var request = new DeletarPessoaRequest { Id = 1 };

        _pessoaRepository.ObterPessoaAsync(request.Id)
            .Returns((PessoaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarPessoaResponse>>().Which
            .Value.Should().BeNull();
    }
}

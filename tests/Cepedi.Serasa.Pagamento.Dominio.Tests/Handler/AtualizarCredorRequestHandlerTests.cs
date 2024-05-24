
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class AtualizarCredorRequestHandlerTests
{
    private readonly ICredorRepository _credorRepository = Substitute.For<ICredorRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<AtualizarCredorRequestHandler> _logger = Substitute.For<ILogger<AtualizarCredorRequestHandler>>();
    private readonly AtualizarCredorRequestHandler _sut;

    public AtualizarCredorRequestHandlerTests()
    {
        _sut = new AtualizarCredorRequestHandler(_credorRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_QuandoCredorExistente_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarCredorRequest { Id = 1, Nome = "Novo Nome" };
        var credorEntity = new CredorEntity { Id = 1, Nome = "Nome Antigo" };
        
        _credorRepository.ObterCredorAsync(request.Id)
            .Returns(credorEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarCredorResponse>>().Which
            .Value.nome.Should().Be(request.Nome);
        await _credorRepository.Received(1).AtualizarCredorAsync(credorEntity);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoCredorInexistente_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarCredorRequest { Id = 1, Nome = "Novo Nome" };
        
        _credorRepository.ObterCredorAsync(request.Id)
            .Returns((CredorEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarCredorResponse>>().Which
            .Value.Should().BeNull();
    }
}

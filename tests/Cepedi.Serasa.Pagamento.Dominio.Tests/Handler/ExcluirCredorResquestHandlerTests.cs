using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class DeletarCredorRequestHandlerTests
{
    private readonly ICredorRepository _credorRepository = Substitute.For<ICredorRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<DeletarCredorRequestHandler> _logger = Substitute.For<ILogger<DeletarCredorRequestHandler>>();
    private readonly DeletarCredorRequestHandler _sut;

    public DeletarCredorRequestHandlerTests()
    {
        _sut = new DeletarCredorRequestHandler(_credorRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveDeletarCredorComSucesso()
    {
        // Arrange
        var request = new DeletarCredorRequest { Id = 1 };

        var credorEntity = new CredorEntity { Id = 1 };

        _credorRepository.ObterCredorAsync(request.Id)
            .Returns(credorEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarCredorResponse>>().Which
            .Value.id.Should().Be(request.Id);
        await _credorRepository.Received(1).ObterCredorAsync(request.Id);
        await _credorRepository.Received(1).DeletarCredorAsync(request.Id);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoCredorNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var request = new DeletarCredorRequest { Id = 1 };

        _credorRepository.ObterCredorAsync(request.Id)
            .Returns((CredorEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarCredorResponse>>().Which
            .Value.Should().BeNull();
    }
}

using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Credor.Dominio.Tests;

public class CriarCredorRequestHandlerTests
{
    private readonly ICredorRepository _credorRepository = Substitute.For<ICredorRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<CriarCredorRequestHandler> _logger = Substitute.For<ILogger<CriarCredorRequestHandler>>();
    private readonly CriarCredorRequestHandler _sut;

    public CriarCredorRequestHandlerTests()
    {
        _sut = new CriarCredorRequestHandler(_credorRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveCriarCredorComSucesso()
    {
        // Arrange
        var request = new CriarCredorRequest { Nome = "Novo Credor" };

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarCredorResponse>>().Which
            .Value.nome.Should().Be(request.Nome);
        await _credorRepository.Received(1).CriarCredorAsync(Arg.Any<CredorEntity>());
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}

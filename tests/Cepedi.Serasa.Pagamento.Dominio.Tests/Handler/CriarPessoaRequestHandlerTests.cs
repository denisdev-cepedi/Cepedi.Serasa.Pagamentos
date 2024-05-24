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

public class CriarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<CriarPessoaRequestHandler> _logger = Substitute.For<ILogger<CriarPessoaRequestHandler>>();
    private readonly CriarPessoaRequestHandler _sut;

    public CriarPessoaRequestHandlerTests()
    {
        _sut = new CriarPessoaRequestHandler(_logger, _pessoaRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveCriarPessoaComSucesso()
    {
        // Arrange
        var request = new CriarPessoaRequest
        {
            Nome = "João",
            Cpf = "12345678900"
        };

        var pessoaEntity = new PessoaEntity
        {
            Id = 1,
            Nome = request.Nome,
            Cpf = request.Cpf
        };

        _pessoaRepository.CriarPessoaAsync(Arg.Any<PessoaEntity>())
            .Returns(pessoaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarPessoaResponse>>().Which
            .Value.Nome.Should().Be(request.Nome);
        await _pessoaRepository.Received(1).CriarPessoaAsync(Arg.Any<PessoaEntity>());
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}

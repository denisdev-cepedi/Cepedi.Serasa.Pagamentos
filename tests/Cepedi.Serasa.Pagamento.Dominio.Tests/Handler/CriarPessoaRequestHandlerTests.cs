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
    private readonly ILogger<CriarPessoaRequestHandler> _logger = Substitute.For<ILogger<CriarPessoaRequestHandler>>();
    private readonly CriarPessoaRequestHandler _sut;

    private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

    public CriarPessoaRequestHandlerTests()
    {
        _sut = new CriarPessoaRequestHandler(_logger, _pessoaRepository, _unitOfWork);
    }

    [Fact]
    public async Task CriarPessoaAsync_QuandoCriar_DeveRetornarSucesso()
    {
        var pessoa = new CriarPessoaRequest { Nome = "Eduardo" };
        _pessoaRepository.CriarPessoaAsync(It.IsAny<PessoaEntity>()).ReturnsForAnyArgs(new PessoaEntity());
        var result = await _sut.Handle(pessoa, CancellationToken.None);

        result.Should().BeOfType<Result<CriarPessoaResponse>>().Which.Value.Nome.Should().Be(pessoa.Nome);
        result.Should().BeOfType<Result<CriarPessoaResponse>>().Which.Value.Nome.Should().NotBeEmpty();
    }
}

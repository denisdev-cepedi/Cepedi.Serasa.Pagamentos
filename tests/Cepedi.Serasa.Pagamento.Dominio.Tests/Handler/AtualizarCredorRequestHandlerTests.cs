
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
    private readonly ILogger<AtualizarCredorRequestHandler> _logger = Substitute.For<ILogger<AtualizarCredorRequestHandler>>();
    private readonly AtualizarCredorRequestHandler _sut;

    private readonly UnitOfWork _unitOfWork = Substitute.For<UnitOfWork>();

    public AtualizarCredorRequestHandlerTests()
    {
        _sut = new AtualizarCredorRequestHandler(_credorRepository, _logger, _unitOfWork);
    }
    [Fact]
    public async Task AtualizarCredor_DeveRetornarSucesso()
    {
        var credor = new AtualizarCredorRequest { Id = 1, Nome = "NovoNome" };
        var credorEntity = new CredorEntity { Id = 1, Nome = "NovoNome" };
        _credorRepository.ObterCredorAsync(It.IsAny<int>()).ReturnsForAnyArgs(new CredorEntity());
        _credorRepository.AtualizarCredorAsync(It.IsAny<CredorEntity>()).ReturnsForAnyArgs(credorEntity);

        var result = await _sut.Handle(credor, CancellationToken.None);
        result.Should().BeOfType<Result<AtualizarCredorResponse>>().Which
            .Value.nome.Should().Be(credor.Nome);

        result.Should().BeOfType<Result<AtualizarCredorResponse>>().Which
            .Value.nome.Should().Be(credorEntity.Nome);
    }
}

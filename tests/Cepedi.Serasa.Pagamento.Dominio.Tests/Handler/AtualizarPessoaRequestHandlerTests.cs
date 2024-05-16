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

public class AtualizarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<AtualizarPessoaRequestHandler> _logger = Substitute.For<ILogger<AtualizarPessoaRequestHandler>>();
    private readonly AtualizarPessoaRequestHandler _sut;

    public AtualizarPessoaRequestHandlerTests(){
        _sut = new AtualizarPessoaRequestHandler(_pessoaRepository, _logger);
    }
    [Fact]
    public async Task AtualizarPessoa_DeveRetornarSucesso()
    {
        var pessoa = new AtualizarPessoaRequest { Id = 1, Nome = "Pericles" };
        var pessoaEntity = new PessoaEntity { Id = 1, Nome = "Pericles" };
        _pessoaRepository.ObterPessoaAsync(It.IsAny<int>()).ReturnsForAnyArgs(new PessoaEntity());
        _pessoaRepository.AtualizarPessoaAsync(It.IsAny<PessoaEntity>()).ReturnsForAnyArgs(pessoaEntity);

        var result = await _sut.Handle(pessoa, CancellationToken.None);
        result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which
            .Value.Nome.Should().Be(pessoa.Nome);

        result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which
            .Value.Nome.Should().Be(pessoaEntity.Nome);
    }
}

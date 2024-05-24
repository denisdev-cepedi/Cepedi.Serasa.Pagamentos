using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using Cepedi.Serasa.Pagamento.Dados;
using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class CriarDividaRequestHandlerTests
{
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly ICredorRepository _credorRepository = Substitute.For<ICredorRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<CriarDividaRequestHandler> _logger = Substitute.For<ILogger<CriarDividaRequestHandler>>();
    private readonly CriarDividaRequestHandler _sut;

    public CriarDividaRequestHandlerTests()
    {
        _sut = new CriarDividaRequestHandler(_logger, _dividaRepository, _credorRepository, _pessoaRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveCriarDividaComSucesso()
    {
        // Arrange
        var request = new CriarDividaRequest
        {
            IdCredor = 1,
            IdPessoa = 1,
            Valor = 100,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        var credorEntity = new CredorEntity { Id = 1 };
        var pessoaEntity = new PessoaEntity { Id = 1 };

        _credorRepository.ObterCredorAsync(request.IdCredor)
            .Returns(credorEntity);
        _pessoaRepository.ObterPessoaAsync(request.IdPessoa)
            .Returns(pessoaEntity);
        _dividaRepository.CriarDividaAsync(Arg.Any<DividaEntity>())
            .Returns(new DividaEntity { Id = 1, IdCredor = 1, IdPessoa = 1, Valor = request.Valor, DataDeVencimento = request.DataDeVencimento });

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarDividaResponse>>().Which
            .Value.Valor.Should().Be(request.Valor);
        await _credorRepository.Received(1).ObterCredorAsync(request.IdCredor);
        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _dividaRepository.Received(2).CriarDividaAsync(Arg.Any<DividaEntity>());
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoCredorNaoExistente_DeveRetornarErro()
    {
        // Arrange
        var request = new CriarDividaRequest
        {
            IdCredor = 1,
            IdPessoa = 1,
            Valor = 100,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        _credorRepository.ObterCredorAsync(request.IdCredor)
            .Returns((CredorEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarDividaResponse>>().Which
            .Value.Should().BeNull();
    }

    [Fact]
    public async Task Handle_QuandoPessoaNaoExistente_DeveRetornarErro()
    {
        // Arrange
        var request = new CriarDividaRequest
        {
            IdCredor = 1,
            IdPessoa = 1,
            Valor = 100,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        var credorEntity = new CredorEntity { Id = 1 };

        _credorRepository.ObterCredorAsync(request.IdCredor)
            .Returns(credorEntity);
        _pessoaRepository.ObterPessoaAsync(request.IdPessoa)
            .Returns((PessoaEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarDividaResponse>>().Which
            .Value.Should().BeNull();
    }
}

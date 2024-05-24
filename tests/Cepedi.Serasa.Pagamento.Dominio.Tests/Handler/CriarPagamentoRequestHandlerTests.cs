using Cepedi.Serasa.Pagamento.Dominio.Entidades;
using Cepedi.Serasa.Pagamento.Dominio.Handlers;
using Cepedi.Serasa.Pagamento.Dominio.Repositorio;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
using Cepedi.Serasa.Pagamento.Compartilhado.Enums;
using Cepedi.Serasa.Pagamento.Dados;

namespace Cepedi.Serasa.Pagamento.Dominio.Tests;

public class CriarPagamentoRequestHandlerTests
{
    private readonly IDividaRepository _dividaRepository = Substitute.For<IDividaRepository>();
    private readonly ICredorRepository _credorRepository = Substitute.For<ICredorRepository>();
    private readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ILogger<CriarPagamentoRequestHandler> _logger = Substitute.For<ILogger<CriarPagamentoRequestHandler>>();
    private readonly CriarPagamentoRequestHandler _sut;

    public CriarPagamentoRequestHandlerTests()
    {
        _sut = new CriarPagamentoRequestHandler(_pagamentoRepository, _credorRepository, _dividaRepository, _logger, _unitOfWork);
    }

    [Fact]
    public async Task Handle_DeveCriarPagamentoComSucesso()
    {
        // Arrange
        var request = new CriarPagamentoRequest
        {
            IdDivida = 1,
            Valor = 100,
            DataDePagamento = DateTime.Now,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        var dividaEntity = new DividaEntity
        {
            Id = 1,
            IdCredor = 1,
            Valor = request.Valor,
            DividaAberta = true
        };

        var credorEntity = new CredorEntity { Id = 1 };

        _dividaRepository.ObterDividaAsync(request.IdDivida)
            .Returns(dividaEntity);
        _credorRepository.ObterCredorAsync(dividaEntity.IdCredor)
            .Returns(credorEntity);
        _pagamentoRepository.QuitarPagamentoAsync(dividaEntity)
            .Returns(dividaEntity);
        _pagamentoRepository.CriarPagamentoAsync(Arg.Any<PagamentoEntity>())
            .Returns(new PagamentoEntity { Id = 1, Valor = request.Valor });

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarPagamentoResponse>>().Which
            .Value.valor.Should().Be(request.Valor);
        await _dividaRepository.Received(1).ObterDividaAsync(request.IdDivida);
        await _credorRepository.Received(1).ObterCredorAsync(dividaEntity.IdCredor);
        await _pagamentoRepository.Received(1).QuitarPagamentoAsync(dividaEntity);
        await _pagamentoRepository.Received(1).CriarPagamentoAsync(Arg.Any<PagamentoEntity>());
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_QuandoValorPagamentoIncompativel_DeveRetornarErro()
    {
        // Arrange
        var request = new CriarPagamentoRequest
        {
            IdDivida = 1,
            Valor = 100,
            DataDePagamento = DateTime.Now,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        var dividaEntity = new DividaEntity
        {
            Id = 1,
            IdCredor = 1,
            Valor = request.Valor + 10, // Valor incompatível
            DividaAberta = true
        };

        _dividaRepository.ObterDividaAsync(request.IdDivida)
            .Returns(dividaEntity);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarPagamentoResponse>>().Which
            .Value.Should().BeNull();
    }

    [Fact]
    public async Task Handle_QuandoErroAoQuitarPagamento_DeveRetornarErro()
    {
        // Arrange
        var request = new CriarPagamentoRequest
        {
            IdDivida = 1,
            Valor = 100,
            DataDePagamento = DateTime.Now,
            DataDeVencimento = DateTime.Now.AddDays(30)
        };

        var dividaEntity = new DividaEntity
        {
            Id = 1,
            IdCredor = 1,
            Valor = request.Valor,
            DividaAberta = true
        };

        _dividaRepository.ObterDividaAsync(request.IdDivida)
            .Returns(dividaEntity);
        _pagamentoRepository.QuitarPagamentoAsync(dividaEntity)
            .Returns((DividaEntity)null); // Simula erro ao quitar pagamento

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarPagamentoResponse>>().Which
            .Value.Should().BeNull();
    }
}

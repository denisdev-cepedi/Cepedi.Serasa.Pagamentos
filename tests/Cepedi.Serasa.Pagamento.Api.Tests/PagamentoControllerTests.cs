using Cepedi.Serasa.Pagamento.Api.Controllers;
using Cepedi.Serasa.Pagamento.Compartilhado.Requests;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Api.Tests
{
    public class PagamentoControllerTests
    {
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly ILogger<PagamentoController> _logger = Substitute.For<ILogger<PagamentoController>>();
        private readonly PagamentoController _sut;

        public PagamentoControllerTests()
        {
            _sut = new PagamentoController(_logger, _mediator);
        }

        [Fact]
        public async Task CriarPagamento_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange
            var request = new CriarPagamentoRequest { Valor = 100, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now };
            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarPagamentoResponse(1, 100)));
            // Act
            await _sut.CriarPagamentoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task AtualizarPagamento_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange
            var request = new AtualizarPagamentoRequest { Valor = 200, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now, IdCredor = 2 };
            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new AtualizarPagamentoResponse(200)));
            // Act
            await _sut.AtualizarPagamentoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task ObterPagamento_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange
            var request = new ObterPagamentoRequest { Id = 10 };
            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new ObterPagamentoResponse(10)));

            // Act
            await _sut.ObterPagamentoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }

        [Fact]
        public async Task DeletePagamento_DeveEnviarRequest_Para_Mediator()
        {
            // Arrange
            var request = new DeletarPagamentoRequest { Id = 5 };
            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new DeletarPagamentoResponse(5)));

            // Act
            await _sut.DeletarPagamentoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }
    }
}



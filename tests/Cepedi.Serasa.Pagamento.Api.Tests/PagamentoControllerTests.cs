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
            var request = new CriarPagamentoRequest { Valor = 100, DataDePagamento = DateTime.Now, DataDeVencimento = DateTime.Now, IdCredor = 1 };
            _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarPagamentoResponse(1, 100)));

            // Act
            await _sut.CriarPagamentoAsync(request);

            // Assert
            await _mediator.ReceivedWithAnyArgs().Send(request);
        }
    }
}



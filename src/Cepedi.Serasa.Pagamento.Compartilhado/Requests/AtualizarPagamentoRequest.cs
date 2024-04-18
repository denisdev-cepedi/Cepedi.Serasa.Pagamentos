using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;
namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class AtualizarPagamentoRequest : IRequest<Result<AtualizarPagamentoResponse>>
{
    public int Id { get; set; }
    public double Valor { get; set; }

    public DateTime DataDePagamento { get; set; }

    public DateTime DataDeVencimento { get; set; }

    public int IdCredor { get; set; }
}

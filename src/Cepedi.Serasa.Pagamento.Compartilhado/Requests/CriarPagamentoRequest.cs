using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;
public class CriarPagamentoRequest : IRequest<Result<CriarPagamentoResponse>>
{
    public int Id { get; set; }
    public double Valor { get; set; } = default!;

    public DateTime DataDePagamento { get; set; }

    public DateTime DataDeVencimento { get; set; }

    public int IdCredor { get; set; }
}

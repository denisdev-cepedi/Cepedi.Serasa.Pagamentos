using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;
public class CriarDividaRequest : IRequest<Result<CriarDividaResponse>>
{
    public double Valor {get; set;}
    public DateTime DataDeVencimento {get; set;}
    public int IdPessoa {get; set;}
    public int IdCredor {get; set;}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Responses;

public record CriarDividaResponse(int IdDivida, double Valor, DateTime DataDeVencimento);

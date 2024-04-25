namespace Cepedi.Serasa.Pagamento.Compartilhado.Responses;

public record CriarDividaResponse(int IdDivida, double Valor, DateTime DataDeVencimento);

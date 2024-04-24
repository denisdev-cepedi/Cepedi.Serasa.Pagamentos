namespace Cepedi.Serasa.Pagamento.Dominio.Entidades;

public class PagamentoEntity
{
    public int Id { get; set; }
    public double Valor { get; set; } = default!;

    public DateTime DataDePagamento { get; set; }

    public DateTime DataDeVencimento { get; set; }

    public int IdCredor { get; set; }

    public CredorEntity Credor { get; set; } = default!;

    internal void AtualizarValor(double valor)
    {
        Valor = valor;
    }
}

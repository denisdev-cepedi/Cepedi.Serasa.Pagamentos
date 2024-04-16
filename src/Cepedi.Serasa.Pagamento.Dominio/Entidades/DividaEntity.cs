namespace Cepedi.Serasa.Pagamento.Dominio.Entidades;

public class DividaEntity
{
    public int Id { get; set; }
    public double Valor { get; set; } = default!;

    public DateTime DataDeVencimento { get; set; }

    public int IdCredor { get; set; }

    public CredorEntity Credor { get; set; } = default!;

}

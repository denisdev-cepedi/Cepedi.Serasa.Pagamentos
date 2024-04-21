namespace Cepedi.Serasa.Pagamento.Dominio.Entidades;

public class DividaEntity
{
    public int Id { get; set; }
    public double Valor { get; set; } = default!;

    public DateTime DataDeVencimento { get; set; }

    public int IdCredor { get; set; }

    public int IdPessoa {get; set; }

    public CredorEntity Credor { get; set; } = default!;
    public PessoaEntity Pessoa {get; set; } = default!;

     internal void AtualizarDados(double valor, DateTime dataDeVencimento)
    {
        Valor = valor;
        DataDeVencimento = dataDeVencimento;
    }
}

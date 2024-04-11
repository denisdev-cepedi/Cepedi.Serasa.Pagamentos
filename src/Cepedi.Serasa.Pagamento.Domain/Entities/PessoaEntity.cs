namespace Cepedi.Serasa.Pagamento.Domain;

public class PessoaEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string? Cpf { get; set; } = null;


}

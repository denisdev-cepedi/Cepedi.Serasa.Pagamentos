namespace Cepedi.Serasa.Pagamento.Dominio.Entidades;

public class PessoaEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string? Cpf { get; set; } = null;

    internal void Atualizar(string nome)
    {
        Nome = nome;
    }


}

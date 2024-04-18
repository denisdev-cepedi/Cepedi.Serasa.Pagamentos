
namespace Cepedi.Serasa.Pagamento.Dominio.Entidades;

public class CredorEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;

    internal void Atualizar(string nome)
    {
        throw new NotImplementedException();
    }
}

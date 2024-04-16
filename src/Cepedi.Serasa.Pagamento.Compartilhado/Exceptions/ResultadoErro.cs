using Cepedi.Serasa.Pagamento.Compartilhado.Enums;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
public class ResultadoErro
{
    public string Titulo { get; set; } = default!;

    public string Descricao { get; set; } = default!;

    public ETipoErro Tipo { get; set; }
}

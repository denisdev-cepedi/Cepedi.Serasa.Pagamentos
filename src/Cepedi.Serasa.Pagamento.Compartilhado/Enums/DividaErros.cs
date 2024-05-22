using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Enums;
public class DividaErros
{
    public static readonly ResultadoErro DividaNaoEncontrada = new()
    {
        Titulo = "Divida não encontrada",
        Descricao = "Verifique os dados informados e tente novamente",
        Tipo = ETipoErro.Alerta
    };

}

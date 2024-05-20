﻿
using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Enums;
public class CredorErrors
{
    public static readonly ResultadoErro Generico = new()
    {
        Titulo = "Ops ocorreu um erro no nosso sistema",
        Descricao = "No momento, nosso sistema está indisponível. Por Favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static readonly ResultadoErro SemResultados = new()
    {
        Titulo = "Sua busca não obteve resultados",
        Descricao = "Tente buscar novamente",
        Tipo = ETipoErro.Alerta
    };

    public static readonly ResultadoErro CredorInexistente = new()
    {
        Titulo = "Credor não encontrado",
        Descricao = "Verifique os dados informados e tente novamente",
        Tipo = ETipoErro.Alerta
    };


    public static ResultadoErro ErroGravacaoCredor = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação do usuário. Por favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro DadosInvalidos = new()
    {
        Titulo = "Dados inválidos",
        Descricao = "Os dados enviados na requisição são inválidos",
        Tipo = ETipoErro.Erro
    };
}
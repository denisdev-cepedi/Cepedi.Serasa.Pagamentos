﻿using Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Excecoes;
public class ExcecaoAplicacao : Exception
{
    public ExcecaoAplicacao(ResultadoErro erro)
     : base(erro.Descricao) => ResponseErro = erro;

    public ResultadoErro ResponseErro { get; set; }
}

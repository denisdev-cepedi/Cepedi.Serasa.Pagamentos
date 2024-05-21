﻿namespace Cepedi.Serasa.Pagamento.Dominio.Servicos;

public interface ICache<T>
{
    Task<T> ObterAsync(string chave);

    Task SalvarAsync(string chave, T objeto, int tempoExpiracao = 10);
}
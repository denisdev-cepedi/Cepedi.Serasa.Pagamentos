﻿using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class CriarCredorRequest : IRequest<Result<CriarCredorResponse>>
{
    public string Nome { get; set; } = default!;

}
﻿using Cepedi.Serasa.Pagamento.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Pagamento.Compartilhado.Requests;

public class ExcluirCredorRequest : IRequest<Result<ExcluirCredorResponse>>
{
    public int Id { get; set; }

}

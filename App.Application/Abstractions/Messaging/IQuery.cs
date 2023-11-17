

using App.Domain.Abstraction;
using MediatR;

namespace App.Application.Abstractions.Messaging;

public interface IQuery<TResponse>: IRequest<Result<TResponse>>
{

}

namespace Northwind.Application.Abstractions;

using ErrorOr;
using MediatR;

public interface IQuery<out TResponse> : IRequest<TResponse>
  where TResponse : IErrorOr { }
namespace Northwind.Application.Abstractions;

using ErrorOr;
using MediatR;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
  where TQuery : IQuery<TResponse>
  where TResponse : IErrorOr { }
namespace Northwind.Application.Abstractions;

using MediatR;

public interface IQuery<TResponse> : IRequest<TResponse> { }
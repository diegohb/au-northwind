namespace Northwind.Application.Abstractions;

using MediatR;

public interface ICommand : IRequest { }

public interface ICommand<out TResponse> : IRequest<TResponse> { }
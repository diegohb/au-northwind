namespace Northwind.Application.Abstractions;

using MediatR;

public interface ICommand : IRequest { }

public interface ICommand<TResponse> : IRequest<TResponse> { }
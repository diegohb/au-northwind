namespace Northwind.Core;

public abstract class DomainException : Exception
{
  protected DomainException(string messageParam) : base(messageParam) { }
}
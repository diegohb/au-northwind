namespace Northwind.Application.Categories;

using Core;

public sealed class MoreThan1CategoryByNameException : DomainException
{
  public MoreThan1CategoryByNameException(string categoryNameParam, int totalCountFoundParam)
    : base($"More than one category found by the same name '{categoryNameParam}'.")
  {
    CategoryName = categoryNameParam;
    TotalCountFound = totalCountFoundParam;
  }

  public string CategoryName { get; }
  public int TotalCountFound { get; }
}
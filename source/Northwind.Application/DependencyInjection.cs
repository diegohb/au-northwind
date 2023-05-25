namespace Northwind.Application;

using Infra.Persistence.EF;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection servicesParam)
  {
    servicesParam.AddInfrastructure();

    return servicesParam;
  }
}
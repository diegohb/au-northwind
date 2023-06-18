namespace Northwind.Application;

using Categories;
using Core.Persistence;
using Infra.Persistence.EF;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection servicesParam)
  {
    servicesParam.AddInfrastructure();

    servicesParam.AddMediatR(a => a.RegisterServicesFromAssemblyContaining<GetCategoriesHandler>());
    servicesParam.AddScoped(typeof(IAggregateRepository<,>), typeof(EventSourcingRepository<,>));

    return servicesParam;
  }
}
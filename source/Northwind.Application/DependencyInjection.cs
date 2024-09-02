namespace Northwind.Application;

using Categories;
using Core.Domain;
using Core.Persistence;
using Core.Persistence.EventStore;
using Infra.Persistence.EF;
using Infra.Persistence.EventStore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection servicesParam, IConfiguration configParam)
  {
    var esConnString = configParam.GetConnectionString("EventStore");
    if (esConnString == null)
    {
      throw new ArgumentNullException(nameof(esConnString));
    }

    servicesParam.AddInfrastructure();

    servicesParam.AddMediatR(a => a.RegisterServicesFromAssemblyContaining<GetCategoriesHandler>());
    servicesParam.AddScoped<IEventStore, ESEventStore>(es => new ESEventStore(esConnString));
    servicesParam.AddScoped(typeof(IDomainMediator<>), typeof(MediatrDomainMediator<>));
    servicesParam.AddScoped(typeof(IAggregateRepository<,>), typeof(EventSourcingRepository<,>));

    return servicesParam;
  }
}
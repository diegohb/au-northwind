namespace Infra.Persistence.EF;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.Core.Persistence;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection servicesParam)
  {
    servicesParam.AddDbContext<NorthwindDbContext>
    (opts =>
    {
      opts.UseSqlServer("name=ConnectionStrings:NorthwindDb", providerOptions => { providerOptions.EnableRetryOnFailure(); });
      opts.LogTo(Console.WriteLine, LogLevel.Information).EnableDetailedErrors().EnableSensitiveDataLogging();
    });

    //servicesParam.AddScoped<IQueryRepository<Category, int>, GenericQueryRepository<Category, int>>();
    servicesParam.AddScoped(typeof(IQueryRepository<,>), typeof(GenericQueryRepository<,>));

    return servicesParam;
  }
}
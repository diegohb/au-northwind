﻿namespace Infra.Persistence.EF;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

    servicesParam.AddScoped(typeof(GenericQueryRepository<,>));

    return servicesParam;
  }
}
﻿namespace Presentation.WebSPA;
    using System;
    using System.IO;
    using ApiConfig;
    using Infra.Persistence.EF;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;

    public class Startup
    {
        public Startup(IConfiguration configParam)
        {
            Configuration = configParam;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder appParam, IWebHostEnvironment envParam)
        {
            if (envParam.IsDevelopment())
            {
                appParam.UseDeveloperExceptionPage();
                appParam.UseBrowserLink();

                //This allows you to debug your ts files in browser using the mappings provided by gulp-typescript
                appParam.UseStaticFiles
                (new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"src")),
                    RequestPath = new PathString("/src")
                });
            }
            else
            {
                appParam.UseExceptionHandler("/Home/Error");
            }

            appParam.UseStaticFiles();

            appParam.UseRouting();

            appParam.UseEndpoints
            (endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection servicesParam)
        {
            servicesParam.AddControllers(opt => opt.UseGeneralRoutePrefix("api"));

            // Add framework services.
            servicesParam.AddRazorPages
            (opts =>
            {
                opts.Conventions.AddPageRoute("/Administration", "Administration/{*route}");
                opts.Conventions.AddPageRoute("/Shopping", "Shopping/{*route}");
            });

            servicesParam.AddLogging
            (pLoggingBuilder =>
            {
                pLoggingBuilder.AddSimpleConsole
                (opts =>
                {
                    opts.IncludeScopes = true;
                    opts.SingleLine = false;
                    opts.ColorBehavior = LoggerColorBehavior.Enabled;
                    opts.TimestampFormat = "hh:mm:ss";
                });
                pLoggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
            });

            servicesParam.AddDbContext<NorthwindDbContext>
            (opts =>
            {
                opts.UseSqlServer("name=ConnectionStrings:NorthwindDb", providerOptions => { providerOptions.EnableRetryOnFailure(); });
                opts.LogTo(Console.WriteLine, LogLevel.Information).EnableDetailedErrors().EnableSensitiveDataLogging();
            });
        }
    }

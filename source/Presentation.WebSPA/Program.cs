namespace Presentation.WebSPA
{
    #region

    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    #endregion

    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] argsParam)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                .ConfigureAppConfiguration
                (builder =>
                {
                    builder.AddJsonFile("appsettings.json", false, true);
                    builder.AddJsonFile("appsettings.{environment}.json", true, true);
                    builder.AddEnvironmentVariables();
                    builder.AddUserSecrets<Program>(true, true);
                });
        }

        public static void Main(string[] argsParam)
        {
            var host = CreateWebHostBuilder(argsParam).Build();
            host.Run();
        }
    }
}
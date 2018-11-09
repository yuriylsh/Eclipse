using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Yuriy.Web.Data;

namespace Yuriy.Web
{
    public class Program
    {
        private const int DbErrorEventId = 1;
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AdPredictiveContext>();
                    context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    services.GetRequiredService<ILogger<Program>>().LogError(
                        DbErrorEventId, 
                        ex, 
                        "Error while configuring database. Make sure Microsoft Sql Server 2016 LocalDB is installed (installed by default with Visual Studio)"
                    );
                }
                   
            }
            host.Run();
        }

        const long OneMegabyte = 1_048_576;
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .Enrich.FromLogContext()
                    .WriteTo.File("log.txt", fileSizeLimitBytes:OneMegabyte, retainedFileCountLimit: 1));
    }
}

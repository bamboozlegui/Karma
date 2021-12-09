using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Karma
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureLogging((hostingContext, builder) =>
                        {
                            builder.AddFile("Logs/Karma-{Date}.txt", LogLevel.Information, null, false, 1073741824, 31, "[{Level:u3}] {Message} {NewLine}");
                        })
                        .UseStartup<Startup>();
                });
    }
}

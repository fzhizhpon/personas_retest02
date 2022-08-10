using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using System.IO;
using System.Net;

namespace Gateway
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
                   webBuilder.ConfigureAppConfiguration((host, config) =>
                   {
                       var c = config.Build();
                       config.AddNacosV2Configuration(c.GetSection("nacosConfig"));

                       if (host.HostingEnvironment.EnvironmentName.Equals("local"))
                       {
                           config.AddOcelot($"{Directory.GetCurrentDirectory()}/ocelot/dev", host.HostingEnvironment);
                       }
                       else
                       {
                           config.AddOcelot($"{Directory.GetCurrentDirectory()}/ocelot/prod", host.HostingEnvironment);
                       }

                   });

                   //Configuracion HTTPS
                   webBuilder.ConfigureKestrel(options =>
                   {
                       var port = 5000;
                       var pfxFilePath = $"{Directory.GetCurrentDirectory()}/wwwroot/ssl/local-linux1.vimasistem.com.pfx";
                       var pfxPassword = "LinuxLocal";

                       options.Listen(IPAddress.Any, port, listenOptions =>
                       {
                           listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                           listenOptions.UseHttps(pfxFilePath, pfxPassword);
                       });
                    });

                    webBuilder.UseStartup<Startup>();
               });
    }
}

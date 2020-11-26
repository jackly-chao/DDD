using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            // The `UseServiceProviderFactory(new AutofacServiceProviderFactory())` call here allows for
            // ConfigureContainer to be supported in Startup with
            // a strongly-typed ContainerBuilder. If you don't
            // have the call to AddAutofac here, you won't get
            // ConfigureContainer support. This also automatically
            // calls Populate to put services you register during
            // ConfigureServices into Autofac.
            //²Î¿¼:https://github.com/autofac/Examples/blob/master/src/AspNetCore3Example/Program.cs
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

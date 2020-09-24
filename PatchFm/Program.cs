using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatchFm.Extensions;
using PatchFm.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PatchFm
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var isDevelopment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false);

            if (isDevelopment)
            {
                builder.AddUserSecrets<Program>();
            }

            builder.AddCommandLine(args);

            var configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services.SetupDI(configuration);

            var patchFm = services.BuildServiceProvider().GetService<IPatchFmService>();
            await patchFm.PatchFm();
        }
    }
}
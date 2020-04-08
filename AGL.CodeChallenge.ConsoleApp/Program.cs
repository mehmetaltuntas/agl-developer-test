using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;

using AGL.CodeChallenge.Common.Configuration;
using AGL.CodeChallenge.Services;

namespace AGL.CodeChallenge.ConsoleApp
{
    /// <summary>
    /// Start up
    /// </summary>
    class Program
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        static IConfigurationRoot Configuration;

        /// <summary>
        /// The entry point for the program
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<CatsAlphabeticalOrder>().GetPersonAndPetsAsync().Wait();

        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddOptions();

            var loggerFactory = new LoggerFactory();               
            services.AddSingleton(loggerFactory);
            services.AddLogging();

            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
            services.AddSingleton(sp => sp.GetService<IOptions<AppSettings>>().Value);

            services.AddHttpClient<IPeopleService, PeopleService>();

            services.AddTransient<CatsAlphabeticalOrder>();
            services.AddSingleton(Console.Out);
            services.AddSingleton(Console.In);
        }
    }
}

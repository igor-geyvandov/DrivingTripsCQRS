using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using MediatR;
using System.Reflection;
using DrivingTripsCQRS.Domain.Driver;
using DrivingTripsCQRS.DataAccess;
using DrivingTripsCQRS.Domain.Trip;
using DrivingTripsCQRS.Domain.DriverReport;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DrivingTripsCQRS.Events;

namespace DrivingTripsCQRS
{
    class Program 
    {
        public static async Task Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<App>>();
            serviceCollection.AddSingleton(typeof(ILogger), logger);

            // entry to run app
            await serviceProvider.GetService<App>().Run(args);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add logging
            // configure logging
            serviceCollection.AddLogging(logging => logging.AddConsole());

            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddSingleton<IDriverRepository, InMemoryDriverRepository>();
            serviceCollection.AddSingleton<ITripRepository, InMemoryTripRepository>();
            serviceCollection.AddSingleton<IDriverReportRepository, InMemoryDriverReportRepository>();
            serviceCollection.AddSingleton<IEventRepository, InMemoryEventRepository>();
            serviceCollection.AddTransient<App>();
        }
    }
}

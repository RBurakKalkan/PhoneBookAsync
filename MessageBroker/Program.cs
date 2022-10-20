
using MassTransit;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GreenPipes;

namespace EventContracts
{
    public interface ValueEntered
    {
        string Value { get; }
    }
}
namespace MessageBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddBus(ConfigureBus);
                        cfg.AddConsumer<ReportConsumer>();
                    });

                    services.AddHostedService<BusService>();
                });
        private static IBusControl ConfigureBus(IServiceProvider provider)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://localhost");

                cfg.ReceiveEndpoint("a-q", e =>
                {
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(x => x.Interval(2, 100));

                    e.Consumer<ReportConsumer>(provider);
                });
            });
        }
    }
}


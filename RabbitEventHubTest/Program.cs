using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitEventHubTest.Consumers;
using MassTransit.MultiBus;
namespace RabbitEventHubTest
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        
                private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(env.ContentRootPath);
                    // config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    // config.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
                    // config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // MassTransit
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<DigestFileReceivedConsumer>();

                        // x.AddConsumers(Gatekeeper.AssemblyHook.Assembly);
                        x.SetKebabCaseEndpointNameFormatter();
                        
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("amqp://guest:guest@localhost:5672/");
                        
                            cfg.ConfigureEndpoints(context);
                        });
                        
                        // x.UsingAzureServiceBus((context, cfg) =>
                        // {
                        //     // cfg.ClearMessageDeserializers();
                        //     // cfg.UseRawJsonSerializer();
                        //
                        //     cfg.Host("Endpoint=sb://dev-cc-constantin-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CKQFXpmBw7vRVXEEvkwC+ojE/+gktMxuCFr1VchFfVA=");
                        //     cfg.ConfigureEndpoints(context);
                        // });
                        
                    //     x.AddRider(rider =>
                    //     {
                    //         rider.AddConsumer<IngestFileReceivedConsumer>();
                    //     
                    //         rider.UsingEventHub((context, k) =>
                    //         {
                    //             k.Host(
                    //                 "Endpoint=sb://dataxgkevents.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=IjOWZQWIZg4iwWHjX5XTahBFaa02TpWcZAQ3QdHJccg=");
                    //     
                    //             k.Storage(
                    //                 "DefaultEndpointsProtocol=https;AccountName=devccdataxdihub;AccountKey=CnPdK3S2nmQmXPUb4BdlDVnvZJvzfhjBj8v+VUc2RLCeoKmIMVMt63Zfdpm208PGLM6mbdxQxVjp+0xH/fUdvQ==;EndpointSuffix=core.windows.net");
                    //     
                    //             k.ReceiveEndpoint("dataxgkevhub", c =>
                    //             {
                    //                 c.ClearMessageDeserializers();
                    //                 c.UseRawJsonSerializer();
                    //                 
                    //                 c.ConfigureConsumer<IngestFileReceivedConsumer>(context);
                    //             });
                    //         });
                    //     });
                    });

                    services.AddMassTransit<ISecondBus>(x =>
                    {
                        x.AddConsumer<IngestFileReceivedConsumer>();
                        //x.AddRequestClient<AllocateInventory>();
                    
                        x.UsingAzureServiceBus((context, cfg) =>
                        {
                            cfg.ClearMessageDeserializers();
                            cfg.UseRawJsonSerializer();
                    
                            cfg.Host("Endpoint=sb://dev-cc-constantin-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CKQFXpmBw7vRVXEEvkwC+ojE/+gktMxuCFr1VchFfVA=");
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    
                    services.AddMassTransitHostedService();
                })
                .UseConsoleLifetime();
    }
    
    public interface ISecondBus :
        IBus
    {
    }
}
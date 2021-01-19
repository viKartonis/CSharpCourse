using System;
using System.Net.Http;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Consumer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
             services.AddSingleton<HttpClient>();
             services.AddSingleton<IExternalAPI, ExternalAPI>();

             services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
             services.AddMassTransit(isp =>
                {
                    var hostConfig = new MassTransitConfigurations();
                    Configuration.GetSection("MassTransit").Bind(hostConfig);

                    return Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(
                            new Uri(hostConfig.RabbitMqAddress), 
                            h =>
                            {
                                h.Username(hostConfig.UserName);
                                h.Password(hostConfig.Password);
                            });

                        cfg.Durable = hostConfig.Durable;
                        cfg.PurgeOnStartup = hostConfig.PurgeOnStartup;

                        cfg.ReceiveEndpoint(host,
                            hostConfig.InputQueue, ep =>
                            {
                                ep.PrefetchCount = 1;
                                ep.ConfigureConsumer<ReceivedBookConsumer>(isp);
                            });
                    });
                },
                ispc =>
                {
                    ispc.AddConsumers(typeof(ReceivedBookConsumer).Assembly);
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
            });
        }
    }
}
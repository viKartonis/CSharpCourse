using System;
using System.Collections.Specialized;
using System.Net.Http;
using BookStorage.DataBase;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WebApplication.Jobs;
using WebApplication.Rabbit;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IProxy, Proxy>();
            services.AddSingleton(isp 
                => new BookContextDbFactory( Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddSingleton<BookConsumer>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMassTransit(isp =>
                {
                    var hostConfig = new MassTransitConfiguration();
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
                                ep.ConfigureConsumer<BookConsumer>(isp);
                            });
                    });
                },
                ispc =>
                {
                    ispc.AddConsumers(typeof(BookConsumer).Assembly);
                });
            
            services.AddSingleton<BookProducer>();
            
            services.AddSingleton<IDataService, DataService>(isp =>
            {
                var service = isp.GetService<BookContextDbFactory>();
                return new DataService(service, 
                    int.Parse(Configuration.GetSection("shopId").Value));
            });
            services.AddSingleton<IJobFactory, InjectableJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>(isp =>
            {
                var properties = new NameValueCollection
                {
                    ["quartz.scheduler.interruptJobsOnShutdownWithWait"] = "true",
                    ["quartz.scheduler.interruptJobsOnShutdown"] = "true"
                };
                return new StdSchedulerFactory(properties);
            });
            services.AddSingleton<SimpleJob>();
            services.AddHostedService<HostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HandwritingService.DAL;
using HandwritingService.DAL.Repositories;
using HandwritingService.Domain;
using KPA.Database.Abstractions;
using System;
using BIED.Messaging.Config;
using BIED.Messaging.Extensions;
using HandwritingService.Web.Messaging.Consumers;

namespace HandwritingService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment enviroment)
        {
            Configuration = configuration;
            Enviroment = enviroment;
        }

        public IConfiguration Configuration { get; set; }
        public IHostEnvironment Enviroment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IRepository<Handwriting>, DummyHandwritingRepository>();

            if (Enviroment.IsEnvironment("Testing"))
            {
                return;
            }

            //services.AddDbContext<HandwritingContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("HandwritingContext")));

            services.AddDbContext<HandwritingContext>(options => options.UseInMemoryDatabase("InMemDB"));
            
            services.Configure<RabbitMqConfig>(Configuration.GetSection("RabbitMq"));

            services.AddRabbitMq();
            services.AddHostedService<NoteCreatedMessageConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HandwritingContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //// Kan onverwachte resultaten opleveren.
            //if (env.IsDevelopment() || env.IsProduction())
            //{
            //    context.Database.Migrate();
            //}

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

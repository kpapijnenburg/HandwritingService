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
using HandwritingService.Logic.Abstract;
using HandwritingService.Logic;
using BIED.Messaging.Abstractions;
using HandwritingService.Web.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            if (Enviroment.IsEnvironment("Testing"))
            {
                return;
            }

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowLocalHost", builder => { builder.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080").AllowAnyHeader().AllowAnyMethod(); });
            });

            if (Enviroment.IsProduction())
            {
                services.AddDbContext<HandwritingContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("HandwritingContext")));

                services.AddTransient<IRepository<Handwriting>, HandwritingRepository>();

                services.Configure<RabbitMqConfig>(Configuration.GetSection("RabbitMq"));

                services.AddRabbitMq();
                services.AddHostedService<NoteCreatedMessageConsumer>();

                // TODO: Replace with real text extractor.
                services.AddSingleton<ITextExtractor, DummyTextExtractor>();
            }

            if (Enviroment.IsDevelopment())
            {
                services.AddDbContext<HandwritingContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocalDB")));

                services.AddTransient<IRepository<Handwriting>, HandwritingRepository>();

                services.AddTransient<IMessageProducer, DummyMessageProducer>();

                services.AddSingleton<ITextExtractor, DummyTextExtractor>();
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HandwritingContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment() || env.IsProduction())
            {
                context.Database.Migrate();
            }

            app.UseRouting();

            app.UseCors("AllowLocalHost");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

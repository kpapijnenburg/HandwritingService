using HandwritingService.DAL;
using HandwritingService.DAL.Repositories;
using HandwritingService.Domain;
using HandwritingService.Web;
using KPA.Database.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;

namespace HandwritingService.IntegrationTests
{
    public class HandwritingServiceFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices((services, _) =>
            {
                services.AddDbContext<HandwritingContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("HandwritingContext")));


                services.AddTransient<IRepository<Handwriting>, HandwritingRepository>();
            });

            base.ConfigureWebHost(builder);
        }
    }
}

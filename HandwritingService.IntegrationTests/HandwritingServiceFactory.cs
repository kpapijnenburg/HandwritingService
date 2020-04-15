using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using KPA.Database.Abstractions;
using HandwritingService.Web;
using HandwritingService.Domain;
using HandwritingService.DAL;
using Microsoft.EntityFrameworkCore;

namespace HandwritingService.IntegrationTests
{
    public class HandwritingServiceFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<HandwritingContext>(options =>
                {
                    options.UseInMemoryDatabase("DummyDB");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<HandwritingContext>();

                    db.Database.EnsureCreated();
                }

            });
        }
    }
}

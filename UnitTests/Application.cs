using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace UnitTests
{
    public class Application : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<IdentityDbContext>));
                services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("Testing", root));

                ServiceProvider serviceProvider = services.BuildServiceProvider();

                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    IServiceProvider scopedServices = scope.ServiceProvider;
                    IdentityDbContext db = scopedServices.GetRequiredService<IdentityDbContext>();

                    db.Database.EnsureCreated();

                    Initializator.InitializeDbForTests(db);
                }
            });

            return base.CreateHost(builder);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ChallengeCSharp.Web;
using ChallengeCSharp.Infrastructure.Persistence; // Ajuste para seu DbContext

namespace ChallengeCSharp.Tests.TestHelpers
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove a configuração real do banco (se houver)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Registra um banco em memória para testes
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Se precisar de outros mocks, registre aqui

                // Constrói o service provider
                var sp = services.BuildServiceProvider();

                // Opcional: inicializa dados no banco em memória
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                    db.Database.EnsureCreated();

                    // Seed inicial (opcional)
                    // SeedDatabase(db);
                }
            });
        }
    }
}
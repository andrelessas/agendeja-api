using Agende.Ja.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Agende.Ja.Api.Configurations
{
    public static class DatabaseInitializerConfiguration
    {
        public static IServiceCollection InitializeDatabase(this IServiceCollection services, string connectionString)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var builderDB = new DbContextOptionsBuilder<AgendeJaContext>();
            builderDB.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();

            // Run migrations
            using (var context = new AgendeJaContext(builderDB.Options))
            {
                Console.WriteLine("Running migrations...");
                context.Database.Migrate();
                Console.WriteLine("All migrations done!");
            }

            services.AddDbContext<AgendeJaContext>(x => x.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

            return services;
        }
    }
}

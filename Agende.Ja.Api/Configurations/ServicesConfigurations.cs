namespace Agende.Ja.Api.Configurations
{
    public static class ServicesConfigurations
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => !p.IsDynamic));

            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            return services;
        }
    }
}

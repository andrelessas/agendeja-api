using Agende.Ja.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};Database={Environment.GetEnvironmentVariable("DB_NAME")};port={Environment.GetEnvironmentVariable("DB_PORT")};User Id={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASS")}";
builder.Services.InitializeDatabase(connectionString)
    .AddDependencyInjectionConfiguration()
    .AddAuthorizationConfiguration()
    .AddServicesConfiguration()
    .AddSwaggerConfigurations();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionsConfigurations>();
});

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "AGENDE JÁ API v1");
    x.RoutePrefix = string.Empty;
    x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

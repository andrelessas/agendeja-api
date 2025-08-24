using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Agende.Ja.Api.Configurations
{
    public static class AuthorizationConfigurations
    {
        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
        {
            //string DEFAULT_POLICY = TypeAcessEnum.Default.ToString();
            //string ADMIN_POLICY = TypeAcessEnum.Administrator.ToString();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Agende_Ja_Api",
                    ValidAudience = "Usuario_Agende_Ja_Api",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET").ToString())),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                /*options.AddPolicy("AdminPadraoOnly", policy =>
                    policy.RequireRole("Administrador", "Padrao"));

                options.AddPolicy("AdminUploaderOnly", policy =>
                    policy.RequireRole("Administrador", "Uploader"));
                //options.AddPolicy("UploaderOnly", policy => policy.RequireRole("Uploader"));
*/
                //options.AddPolicy("AdminOnly", policy => policy.RequireRole(ADMIN_POLICY));
                //options.AddPolicy("DefaultOnly", policy => policy.RequireRole(DEFAULT_POLICY));
            });

            return services;
        }
    }
}

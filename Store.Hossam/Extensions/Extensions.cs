using Domain.Contracts;
using Domain.Exceptions.Validations;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presistance;
using Presistance.Data.Identity;
using Services;
using Shared;
using Store.Hossam.Middelwares;
using Microsoft.IdentityModel.Tokens;


namespace Store.Hossam.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection ApplyAllServices(this IServiceCollection Services, IConfiguration configuration)
        {


            Services.AddBuiltInServices();

            Services.AddSwagerServices();

            Services.AddIdentityServices();

            Services.AddInferastructureServicseRegistration(configuration);

            Services.AddApplicationServicesRegistrations(configuration);

            Services.ApplyConfigrationServices();

            Services.AddJwtOptionsServices(configuration);




            return Services;
        }

        public static async Task<WebApplication> ApplyAllMiddelwares(this WebApplication app)
        {


            app.UseGlobalErrorHandling();

            await app.AddInilitzedDataBase();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();




            app.MapControllers();

            return app;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection Services)
        {
            Services.AddControllers();
            return Services;
        }
        private static IServiceCollection AddJwtOptionsServices(this IServiceCollection Services, IConfiguration configuration)
        {
            var jwtoptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,


                ValidIssuer = jwtoptions.Issuer,
                ValidAudience = jwtoptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtoptions.SecretKey))
            });
            return Services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return Services;
        }
        private static IServiceCollection AddSwagerServices(this IServiceCollection Services)
        {

            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }
        private static IServiceCollection ApplyConfigrationServices(this IServiceCollection Services)
        {

            Services.Configure<ApiBehaviorOptions>(Config =>
            {
                Config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any()).Select(m => new ValdiationError()
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                    });


                    var response = new ValdationErrorResponse()
                    {
                        Error = errors
                    };
                    return new BadRequestObjectResult(response);
                };

            }
    );
            return Services;
        }
        private static async Task<WebApplication> AddInilitzedDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            {
                var dbIniltilizer = scope.ServiceProvider.GetRequiredService<IDbIniltilizer>();
                await dbIniltilizer.InilitizerAsync();
                await dbIniltilizer.InilitizerIdentityAsync();
            }
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddelwares>();

            return app;
        }

    }
}

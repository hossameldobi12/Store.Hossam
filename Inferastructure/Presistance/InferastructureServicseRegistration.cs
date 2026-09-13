using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistance.Data.Contexts;
using Presistance.Data.Identity;
using Presistance.Data.Reposiotries;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public static class InferastructureServicseRegistration
    {
        public static IServiceCollection AddInferastructureServicseRegistration(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<StoreDbContext>(Options =>
            Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            Services.AddDbContext<StoreIdentityDbContext>(Options =>
            Options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            Services.AddScoped<IDbIniltilizer, DbInitilizer>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<ICacheReposiotry, CacheReposiotry>();
            Services.AddSingleton<IConnectionMultiplexer>(servicesProvider =>
                 ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));


            return Services;
        }
    }
}

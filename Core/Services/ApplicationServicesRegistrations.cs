using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Services.Services;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistrations
    {
        public static IServiceCollection AddApplicationServicesRegistrations(this IServiceCollection Services,IConfiguration configuration)
        {


            Services.AddAutoMapper(cfg =>
              {

              }, typeof(AssemblyReference).Assembly);
           
            Services.AddScoped<IServicesManger, ServicesManger>();
            Services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return Services;
        }
    }
}

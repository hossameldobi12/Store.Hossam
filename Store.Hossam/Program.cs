
using Domain.Contracts;
using Domain.Exceptions.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data.Contexts;
using Services;
using Services.Abstractions;
using Services.Services;
using Store.Hossam.Extensions;
using Store.Hossam.Middelwares;
using System.Reflection.Metadata;
using AssemblyReference = Services.AssemblyReference;
namespace Store.Hossam
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.ApplyAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ApplyAllMiddelwares();

            app.Run();
        }
    }
}

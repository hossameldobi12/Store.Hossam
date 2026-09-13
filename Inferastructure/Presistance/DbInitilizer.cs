using Domain.Contracts;
using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance.Data.Contexts;
using Presistance.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DbInitilizer : IDbIniltilizer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _storeIdentityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitilizer(StoreDbContext storeDbContext, StoreIdentityDbContext storeIdentityDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _context = storeDbContext;
            _storeIdentityDbContext = storeIdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InilitizerAsync()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {

                await _context.Database.MigrateAsync();

            }
            if (!_context.productTypes.Any())
            {
                var TypeData = await File.ReadAllTextAsync(@"..\Inferastructure\Presistance\Data\DataSeeding\types.json");
                var Types = JsonSerializer.Deserialize<IEnumerable<ProductType>>(TypeData);
                if (Types is not null && Types.Any())
                {
                    await _context.productTypes.AddRangeAsync(Types);
                    await _context.SaveChangesAsync();
                }
            }
            if (!_context.productBrands.Any())
            {
                var BrandsData = await File.ReadAllTextAsync(@"..\Inferastructure\Presistance\Data\DataSeeding\brands.json");
                var Brands = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(BrandsData);
                if (Brands is not null && Brands.Any())
                {
                    await _context.productBrands.AddRangeAsync(Brands);
                    await _context.SaveChangesAsync();
                }
            }
            if (!_context.products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync(@"..\Inferastructure\Presistance\Data\DataSeeding\products.json");
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(ProductsData);
                if (products is not null && products.Any())
                {
                    await _context.products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }
            if (!_context.deliveryMethod.Any())
            {
                var DeliveryData = await File.ReadAllTextAsync(@"..\Inferastructure\Presistance\Data\DataSeeding\delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<IEnumerable<DeliveryMethod>>(DeliveryData);
                if (deliveryMethods is not null && deliveryMethods.Any())
                {
                    await _context.deliveryMethod.AddRangeAsync(deliveryMethods);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public  async Task InilitizerIdentityAsync()
        {
            if (_storeIdentityDbContext.Database.GetPendingMigrations().Any())
            {
                await _storeIdentityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

            }
            if (!_userManager.Users.Any())
            {
                var SuperAdmin = new AppUser
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@Gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01065238544"

                };
                var Admin = new AppUser
                {
                    DisplayName = "Admin",
                    Email = "Admin@Gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01065238544"

                };
                await _userManager.CreateAsync(Admin, "Admin@123");
                await _userManager.CreateAsync(SuperAdmin, "SuperAdmin@123");

                await _userManager.AddToRoleAsync(Admin, "Admin");
                await _userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");
            }
        }

    }
}

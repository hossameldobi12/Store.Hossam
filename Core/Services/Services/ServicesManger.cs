using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServicesManger(IUnitOfWork unitOfWork,
                                IBasketRepository basketRepository,
                                ICacheReposiotry cacheRepository,
                                IMapper mapper,
                                UserManager<AppUser> user,
                                IOptions<JwtOptions> jwtOptions,
                                IConfiguration configuration)
                                : IServicesManger
    {
        public IProductService Product { get; } = new ProductService(unitOfWork, mapper);

        public IBasketServices Basket { get; } = new BasketServicse(basketRepository, mapper);

        public ICacheServices Cache { get; } = new CacheServices(cacheRepository);

        public IAuthService AuthService { get; } = new AuthServices(user,jwtOptions,mapper);

        public IOrderServices orderService { get; } = new OrderServices(unitOfWork,mapper,basketRepository);

        public IPaymentServices Payment { get; } = new PaymentServices(basketRepository, unitOfWork, mapper,configuration );
    }
}

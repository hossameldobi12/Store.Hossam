using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.NotFound;
using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Shared.Dto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProduct = Domain.Models.Product;

namespace Services.Services
{
    public class PaymentServices(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper,IConfiguration configuration) : IPaymentServices
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            var basket = await basketRepository.GetBasketAsync(BasketId);
            if (basket == null) { throw new BasketNotFoundExcepton(BasketId); }


            foreach (var item in basket.Items)
            {
                var productItem = await unitOfWork.GetReposiotry<OrderProduct, int>().GetByIdAsync(item.Id);
                if (productItem == null) { throw new ProductNotFoundException(item.Id); }
                item.Price = productItem.Price;
            }


            if (!basket.DeleveryMethodId.HasValue) { throw new Exception("Delivery method is not selected"); }
            var deleveryMethod = await unitOfWork.GetReposiotry<DeliveryMethod, int>().GetByIdAsync(basket.DeleveryMethodId.Value);
            if (deleveryMethod == null) { throw new DeliveryMethodNotFoundException(basket.DeleveryMethodId.Value); }
            basket.ShippingPrice = deleveryMethod.Cost;

            var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;



            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            var service = new PaymentIntentService();
            if (String.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await basketRepository.UpdateBasketAsync(basket);
            var result = mapper.Map<BasketDto>(basket);
            return result;


        }
    }
}

using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Domain.Models;
using Services.Abstractions;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BasketServicse(IBasketRepository basketRepository , IMapper mapper) : IBasketServices
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var result = await basketRepository.GetBasketAsync(id);
            if(result == null) throw new BasketNotFoundExcepton(id);
          var resultDto =  mapper.Map<BasketDto>(result);
            return resultDto;
            
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto)
        {
            var result = mapper.Map<CustomerBasket?>(basketDto);
          result = await  basketRepository.UpdateBasketAsync(result);
            if (result is null) throw new BasketBadRequestException();
            var Basket = mapper.Map<BasketDto>(result);
            return Basket;
        }
        public  async Task<bool> DeleteBasketAsync(string id)
        {
         var flag =  await  basketRepository.DeleteBasketAsync(id);
            if (flag == false) throw new BasketDeleteBadRequestException();
            return flag;
        }


    }
}

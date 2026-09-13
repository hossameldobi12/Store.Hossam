using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared.Dto.OrdersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OrderServices(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IOrderServices
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderDto, string userEmail)
        {
            var address = mapper.Map<ShippingAddress>(orderDto.Address);

            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId);
            if (basket == null) { throw new BasketNotFoundExcepton(orderDto.BasketId); }

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetReposiotry<Product, int>().GetByIdAsync(item.Id);
                if (product == null) { throw new ProductNotFoundException(item.Id); }

                var OrddrItem = new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), product.Price, item.Quantity);
                orderItems.Add(OrddrItem);
            }

            var delivery = await unitOfWork.GetReposiotry<DeliveryMethod, int>().GetByIdAsync(orderDto.delveryMethodId);
            if (delivery == null) { throw new DeliveryMethodNotFoundException(orderDto.delveryMethodId); }

            var subtotal = orderItems.Sum(i => i.Price * i.Quantity);
            
            var spec = new OrderWithPaymentIntint(basket.PaymentIntentId);
            var existingOrder = await unitOfWork.GetReposiotry<Order, Guid>().GetByIdAsync(spec);
            if (existingOrder is not null)
            {
               unitOfWork.GetReposiotry<Order, Guid>().Delete(existingOrder);
            }

            var order = new Order(userEmail, address, orderItems, delivery, subtotal, basket.PaymentIntentId);


            await unitOfWork.GetReposiotry<Order, Guid>().AddAsync(order);
            var flag = await unitOfWork.SaveChangeAsync();
            if (flag == null) { throw new OrderCreateBadRequestException(); }

            var OrderReturnDto = mapper.Map<OrderResultDto>(order);
            return OrderReturnDto;
        }


        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var result = await unitOfWork.GetReposiotry<DeliveryMethod, int>().GetAllAsync();
            var deliverDto = mapper.Map<IEnumerable<DeliveryMethodDto>>(result);
            return deliverDto;

        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecififcation(id);
            var result = await unitOfWork.GetReposiotry<Order, Guid>().GetByIdAsync(spec);
            if (result == null) { throw new OrderNotFoundException(id); }
            var ResultDto = mapper.Map<OrderResultDto>(result);
            return ResultDto;

        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail)
        {
            var spec = new OrderSpecififcation(userEmail);
            var result = await unitOfWork.GetReposiotry<Order, Guid>().GetAllAsync(spec);
            var ResultDto = mapper.Map<IEnumerable<OrderResultDto>>(result);
            return ResultDto;
        }
    }
}

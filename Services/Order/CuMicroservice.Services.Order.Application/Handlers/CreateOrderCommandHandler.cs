using CuMicroservice.Services.Order.Application.Commands;
using CuMicroservice.Services.Order.Application.Dtos;
using CuMicroservice.Services.Order.Domain.OrderAggregate;
using CuMicroservice.Services.Order.Infrastructure;
using CuMicroservice.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.AddressDto.Province, request.AddressDto.District, request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);
            Domain.OrderAggregate.Order newOrder = new(newAddress, request.BuyerId);
            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });
            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync();
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}

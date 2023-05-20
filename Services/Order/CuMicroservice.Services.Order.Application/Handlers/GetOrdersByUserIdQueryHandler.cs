using CuMicroservice.Services.Order.Application.Dtos;
using CuMicroservice.Services.Order.Application.Mapping;
using CuMicroservice.Services.Order.Application.Queries;
using CuMicroservice.Services.Order.Infrastructure;
using CuMicroservice.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _dbContext;
        public GetOrdersByUserIdQueryHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            if (!orders.Any()) return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            return Response<List<OrderDto>>.Success(ObjectMapper.Mapper.Map<List<CuMicroservice.Services.Order.Domain.OrderAggregate.Order>, List<OrderDto>>(orders), 200);
        }
    }
}

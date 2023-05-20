using CuMicroservice.Services.Order.Application.Dtos;
using CuMicroservice.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}

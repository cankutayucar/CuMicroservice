using CuMicroservice.Services.Order.Application.Dtos;
using CuMicroservice.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto AddressDto { get; set; }
    }
}

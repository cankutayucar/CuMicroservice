﻿using System.Collections.Generic;
using System.Linq;

namespace CuMicroservice.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice { get => BasketItems.Sum(bi => bi.Price * bi.Quantity); }
    }
}

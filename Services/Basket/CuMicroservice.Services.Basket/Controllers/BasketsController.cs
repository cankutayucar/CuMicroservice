using CuMicroservice.Services.Basket.Dtos;
using CuMicroservice.Services.Basket.Services;
using CuMicroservice.Shared.ControllerBases;
using CuMicroservice.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            return CreateActionResultInstance(await _basketService.SaveOrUpdate(basketDto));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return CreateActionResultInstance(await _basketService.DeleteBasket(_sharedIdentityService.GetUserId));
        }
    }
}

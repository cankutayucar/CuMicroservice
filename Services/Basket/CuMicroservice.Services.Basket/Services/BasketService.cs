using CuMicroservice.Services.Basket.Dtos;
using CuMicroservice.Shared.Dtos;
using System.Text.Json;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> DeleteBasket(string UserId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(UserId);
            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Basket not found", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string UserId)
        {
            var existbasket = await _redisService.GetDb().StringGetAsync(UserId);
            if (string.IsNullOrEmpty(existbasket))
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existbasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Basket could not update or save", 500);
        }
    }
}

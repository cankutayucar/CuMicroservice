using CuMicroservice.Services.Basket.Dtos;
using CuMicroservice.Shared.Dtos;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string UserId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> DeleteBasket(string UserId);
    }
}

using CuMicroservice.Shared.Dtos;
using CuMicroservice.Web.Models;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace CuMicroservice.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}

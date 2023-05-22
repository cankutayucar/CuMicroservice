using CuMicroservice.Shared.Dtos;
using CuMicroservice.Web.Models;
using CuMicroservice.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace CuMicroservice.Web.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor contextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            throw new System.NotImplementedException();
        }

        public Task RevokeRefreshToken()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<bool>> SignIn(SignInInput signInInput)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

        }
    }
}

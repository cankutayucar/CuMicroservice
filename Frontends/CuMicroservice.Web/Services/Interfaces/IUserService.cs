using CuMicroservice.Web.Models;
using System.Threading.Tasks;

namespace CuMicroservice.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}

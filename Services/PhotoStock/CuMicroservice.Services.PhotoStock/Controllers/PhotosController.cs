using CuMicroservice.Services.PhotoStock.Dtos;
using CuMicroservice.Shared.ControllerBases;
using CuMicroservice.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CuMicroservice.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotosController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);
                var returnPath = "photos/" + photo.FileName;
                PhotoDto photoDto = new() { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
        }


        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail($"{photoUrl} url bulunamadı", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }


    }
}

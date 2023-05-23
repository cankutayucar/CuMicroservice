using CuMicroservice.Web.Models;
using CuMicroservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CuMicroservice.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;
        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signInInput)
        {
            if (!ModelState.IsValid)
            {
                return View(signInInput);
            }
            var response = await _identityService.SignIn(signInInput);
            if(!response.IsSuccesful)
            {
                response.Errors.ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error);
                });
                return View();
            }

            return RedirectToAction("Index","Home");
        }
    }
}

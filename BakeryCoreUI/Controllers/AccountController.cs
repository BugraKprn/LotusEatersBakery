using Bakery.Library.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Bakery.Library.Dtos;
using Bakery.Library.Services;
using Bakery.Library.Entity;

namespace BakeryCoreUI.Controllers
{
    public class AccountController : Controller
    {
        private ILogger<AccountController> _logger;
        private IAdminService _adminService;
        public AccountController(
            ILogger<AccountController> logger,
            IAdminService adminService
            )
        {
            _logger = logger;
            _adminService = adminService;
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("girisyap")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GirisYap(LoginDto dto)
        {
            ApiResult result = new ApiResult();
            try
            {
                if (string.IsNullOrEmpty(dto.UserName) | string.IsNullOrEmpty(dto.Password))
                {
                    throw new Exception("Lütfen kullanıcı adı veya şifrenizi giriniz");
                }

                Admin user = _adminService.GetAdminUser(dto.UserName, dto.Password);

                if (user.Id == 0)
                {
                    throw new Exception("kullanıcı adı veya şifre bilginiz yanlış");
                }

                var claims = new List<Claim>{
                            new(ClaimTypes.Name, dto.UserName),
                            new(ClaimTypes.Role, "Admin")
                        };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                result.Success = true;
                result.Message = "";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            //Write your Insert code here;
            return await Task.FromResult(Ok(result));
        }

        [Route("logout")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SingOut()
        {
            ApiResult result = new ApiResult();
            try
            {
                await HttpContext.SignOutAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            //Write your Insert code here;
            return await Task.FromResult(Ok(result));



        }
    }
}

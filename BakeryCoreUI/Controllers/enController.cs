using Bakery.Library.Entity;
using Bakery.Library.Helpers;
using Bakery.Library.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace BakeryCoreUI.Controllers
{
    public class enController : Controller
    {
        private readonly ILogger<enController> _logger;
        private ISettingService _settingService;
        private IQuickLinkService _quickLinkService;
        private IProductService _productService;
        private IMailListService _mailListService;
        private IContactService _contactService;
        public enController(
            ILogger<enController> logger
            , ISettingService settingService
            , IQuickLinkService quickLinkService
            , IProductService productService
            , IMailListService mailListService
            , IContactService contactService)
        {
            _logger = logger;
            _settingService = settingService;
            _quickLinkService = quickLinkService;
            _productService = productService;
            _mailListService = mailListService;
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            var productModel = _productService.GetList();
            return View(productModel);
        }

        [Route("en/productdetail/{title}/{id}")]
        public IActionResult ProductDetails(int id)
        {
            var urun = _productService.GetById(id);
            ViewData["Title"] = urun.ProductName;
            return View(urun);
        }

        public IActionResult About()
        {
            ViewData["Title"] = "About";
            var setting = _settingService.Get();
            return View(setting);
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact";
            var settingModel = _settingService.Get();
            return View(settingModel);
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacy Policy";
            var settingModel = _settingService.Get();
            return View(settingModel);
        }

        public IActionResult TermsOfService()
        {
            ViewData["Title"] = "Terms Of Service";
            var settingModel = _settingService.Get();
            return View(settingModel);
        }

        public IActionResult RefundPolicy()
        {
            ViewData["Title"] = "Refund Policy";
            var settingModel = _settingService.Get();
            return View(settingModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SubscribeMail(MailList dto)
        {
            ApiResult result = new ApiResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Email fields are required\r\nPlease enter a valid email address");
                }
                else
                {
                    _mailListService.Create(dto);
                    result.Success = true;
                    result.Message = "You have successfully subscribed";
                }
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ContactPost(Contact dto)
        {
            ApiResult result = new ApiResult();
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Fill in the required fields");
                }
                else
                {
                    _contactService.Create(dto);
                    result.Success = true;
                    result.Message = "Success";
                }
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

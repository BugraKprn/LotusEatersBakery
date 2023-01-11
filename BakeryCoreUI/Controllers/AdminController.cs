using Bakery.Library.Helpers;
using Bakery.Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrionDAL.OAL;
using OrionDAL.Web.Entities.Core;
using System.ComponentModel.Design;

namespace BakeryCoreUI.Controllers
{
    public class AdminController : Controller
    {
        private IMailListService _mailListService;
        private IContactService _contactService;
        private IProductService _productService;
        private IQuickLinkService _quickLinkService;
        private ISettingService _settingService;
        private ILogger<AdminController> _logger;

        public AdminController(
            IMailListService mailListService,
            IContactService contactService,
            IProductService productService,
            IQuickLinkService quickLinkService,
            ISettingService settingService,
            ILogger<AdminController> logger)
        {
            _mailListService = mailListService;
            _contactService = contactService;
            _productService = productService;
            _quickLinkService = quickLinkService;
            _settingService = settingService;
            _logger = logger;
        }

        [Route("admin")]
        [Authorize]
        public IActionResult Index()
        {
            ViewData["Title"] = "Anasayfa";
            return View();
        }

        [Authorize]
        public IActionResult SettingPage()
        {
            ViewData["Title"] = "Settings";
            return View();
        }

        [Route("getsetting")]
        [HttpGet]
        public async Task<IActionResult> GetSetting()
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = _settingService.Get();
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

        [Authorize]
        public IActionResult ProductPage()
        {
            ViewData["Title"] = "Product";
            return View();
        }

        [Route("getproducts")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = _productService.GetList();
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

        [Authorize]
        public IActionResult LinkPage()
        {
            ViewData["Title"] = "QuickLinks";
            return View();
        }

        [Route("getquicklinks")]
        [HttpGet]
        public async Task<IActionResult> GetLinks()
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = _quickLinkService.GetList();
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

        [Authorize]
        public IActionResult NewsletterPage()
        {
            ViewData["Title"] = "Newsletter";
            return View();
        }

        [Route("getnewsletter")]
        [HttpGet]
        public async Task<IActionResult> GetNewsletter()
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = _mailListService.GetList();
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

        [Authorize]
        public IActionResult ContactPage()
        {
            ViewData["Title"] = "Contact";
            return View();
        }

        [Route("getcontact")]
        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = _contactService.GetList();
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

        [HttpPost]
        public string UploadFile(IFormFile myFile)
        {

            var targetLocation = Environment.CurrentDirectory + "/StaticFiles";
            string dosyaUzantisi = Path.GetExtension(myFile.FileName).ToLower();
            Guid g = Guid.NewGuid();


            try
            {
                using (var fileStream = System.IO.File.Create(targetLocation + "/" + g.ToString() + dosyaUzantisi))
                {
                    myFile.CopyTo(fileStream);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
            return g.ToString() + dosyaUzantisi;
        }
        [HttpPost]
        public string UploadBigImageFile(IFormFile myBigImageFile)
        {

            var targetLocation = Environment.CurrentDirectory + "/StaticFiles";
            string dosyaUzantisi = Path.GetExtension(myBigImageFile.FileName).ToLower();
            Guid g = Guid.NewGuid();


            try
            {
                using (var fileStream = System.IO.File.Create(targetLocation + "/" + g.ToString() + dosyaUzantisi))
                {
                    myBigImageFile.CopyTo(fileStream);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
            return g.ToString() + dosyaUzantisi;
        }
        public JsonResult OrionSave(IFormCollection form)
        {
            var values = form["values"];
            var table = form["tablo"];
            var keyValue = Convert.ToInt32(form["key"]);

            var typeTable = OrionDAL.OAL.Metadata.DataDictionary.Instance.GetTypeofEntity(table);
            var obj = (BaseEntity)(keyValue > 0 ? Transaction.Instance.Read(typeTable, keyValue) : Activator.CreateInstance(typeTable));

            JsonConvert.PopulateObject(values, obj);
            if (table == "Setting" && obj.Id == 0)
            {
                var setting = _settingService.Get();
                if (setting.Id == 0)
                {
                    obj.Save();
                }
                else
                {
                   _logger.LogError("Ayar tablosuna sadece 1 adet kayıt girebilirsiniz.");
                }
            }
            else
            {
                obj.Save();
            }
            return Json(new { data = obj });
        }

        public JsonResult OrionDelete(IFormCollection form)
        {
            var table = form["tablo"];
            var keyValue = Convert.ToInt32(form["key"]);

            var typeTable = OrionDAL.OAL.Metadata.DataDictionary.Instance.GetTypeofEntity(table);
            var obj = (BaseEntity)Transaction.Instance.Read(typeTable, keyValue);
            //if (table == "BagisTanim")
            //{
            //    Transaction.Instance.ExecuteNonQuery("delete from BagisSepeti where BagisTanim_Id=@prm0", obj.Id);
            //}
            //else if (table == "Ogrenci")
            //{
            //    Transaction.Instance.ExecuteNonQuery("delete from BagisSepeti where OgrenciId=@prm0", obj.Id);
            //    Transaction.Instance.ExecuteNonQuery("delete from BasariliOdemeLog where OgrenciId=@prm0", obj.Id);
            //    Transaction.Instance.ExecuteNonQuery("delete from BasarisizOdemeLog where OgrenciId=@prm0", obj.Id);
            //    Transaction.Instance.ExecuteNonQuery("delete from OdemeAraTablosu where OgrenciId=@prm0", obj.Id);
            //    Transaction.Instance.ExecuteNonQuery("delete from OgrenciBurs where Ogrenci_Id=@prm0", obj.Id);
            //}
            obj.Delete();

            return Json(new { data = "{}" });
        }
    }
}

using Bakery.Library.Services;
using Bakery.Library.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BakeryCoreUI.ViewComponents
{
    public class Header : ViewComponent
    {
        private readonly ISettingService _service;
        public Header(
            ISettingService service)
        {
            _service = service;
        }
        public IViewComponentResult Invoke()
        {
            HeaderDto model = new HeaderDto()
            {
                SiteSetting = _service.Get()
            };
            return View(model);
        }
    }

    public class HeaderDto
    {
        public Setting SiteSetting { get; set; } = new Setting();
    }
}

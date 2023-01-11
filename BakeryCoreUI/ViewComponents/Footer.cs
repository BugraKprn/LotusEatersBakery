using Bakery.Library.Entity;
using Bakery.Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeryCoreUI.ViewComponents
{
    public class Footer : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IQuickLinkService _linkService;

        public Footer(
            ISettingService settingService, IQuickLinkService linkService)
        {
            _settingService = settingService;
            _linkService = linkService;
        }
        public IViewComponentResult Invoke()
        {
            FooterDto model = new FooterDto()
            {
                FooterLink = _linkService.GetList(),
                SiteSetting = _settingService.Get()
            };
            return View(model);
        }
    }
    public class FooterDto
    {
        public Setting SiteSetting { get; set; } = new Setting();
        public List<QuickLink> FooterLink { get; set; } = new List<QuickLink>() { }; 
    }
}

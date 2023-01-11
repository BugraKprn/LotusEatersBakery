namespace BakeryCoreUI.Models
{
    public class FooterModel
    {
        public List<Bakery.Library.Entity.QuickLink> LinkList { get; set; } = new List<Bakery.Library.Entity.QuickLink>();
        public Bakery.Library.Entity.Setting Setting { get; set; } = new Bakery.Library.Entity.Setting();
    }
}

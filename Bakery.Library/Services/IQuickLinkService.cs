using Bakery.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Services
{
    public interface IQuickLinkService
    {
        List<QuickLink> GetList();
        void Create(QuickLink link);
        void Delete(int id);
    }
}

using Bakery.Library.Entity;
using Bakery.Library.Services;
using OrionDAL.OAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Manager
{
    public class QuickLinkManager : IQuickLinkService
    {
        public void Create(QuickLink link)
        {
            link.Save();
        }

        public void Delete(int id)
        {
            Transaction.Instance.ExecuteNonQuery("Delete from QuickLink where Id = @prm0", id);
        }

        public List<QuickLink> GetList()
        {
            return Transaction.Instance.ReadList<QuickLink>("Select * from QuickLink order by LinkOrder asc").ToList() ?? new List<QuickLink>();
        }
    }
}

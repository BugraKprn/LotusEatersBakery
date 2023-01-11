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
    public class MailListManager : IMailListService
    {
        public void Create(MailList mail)
        {
            mail.Save();
        }

        public void Delete(int id)
        {
            Transaction.Instance.ExecuteNonQuery("Delete from MailList where Id = @prm0", id);
        }

        public List<MailList> GetList()
        {
            return Transaction.Instance.ReadList<MailList>("Select * from MailList").ToList() ?? new List<MailList>();
        }
    }
}

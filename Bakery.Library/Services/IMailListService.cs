using Bakery.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Services
{
    public interface IMailListService
    {
        List<MailList> GetList();
        void Create(MailList mail);
        void Delete(int id);
    }
}

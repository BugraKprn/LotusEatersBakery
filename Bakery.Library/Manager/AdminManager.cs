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
    public class AdminManager : IAdminService
    {
        public void Create(Admin admin)
        {
            admin.Save();
        }

        public Admin Get()
        {
            return Transaction.Instance.Read<Admin>("Select top 1 * from Admin")?? new Admin();
        }

        public Admin GetAdminUser(string userName, string password)
        {
            return Transaction.Instance.Read<Admin>("select * from Admin where UserName=@prm0 and Password=@prm1", userName, password) ?? new Admin();
        }
    }
}

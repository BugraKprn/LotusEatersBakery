using Bakery.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Services
{
    public interface IAdminService
    {
        Admin Get();
        void Create(Admin admin);
        Admin GetAdminUser(string userName,string password);
    }
}

using Bakery.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Services
{
    public interface IContactService
    {
        List<Contact> GetList();
        void Create(Contact contact);
        void Delete(int id);
    }
}

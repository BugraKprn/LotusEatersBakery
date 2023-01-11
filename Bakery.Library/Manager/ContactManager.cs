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
    public class ContactManager : IContactService
    {
        public void Create(Contact contact)
        {
            contact.Save();
        }

        public void Delete(int id)
        {
            Transaction.Instance.ExecuteNonQuery("Delete from Contact where Id = @prm0", id);
        }

        public List<Contact> GetList()
        {
            return Transaction.Instance.ReadList<Contact>("Select * from Contact").ToList() ?? new List<Contact>();
        }
    }
}

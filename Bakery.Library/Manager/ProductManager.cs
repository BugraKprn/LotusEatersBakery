using Azure;
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
    public class ProductManager : IProductService
    {
        public void Create(Product product)
        {
            product.Save();
        }

        public void Delete(int id)
        {
            Transaction.Instance.ExecuteNonQuery("Delete from Product where Id = @prm0", id);
        }

        public List<Product> GetList()
        {
            return Transaction.Instance.ReadList<Product>("Select * from Product").ToList() ?? new List<Product>();
        }
        public Product GetById(int id)
        {
			return Transaction.Instance.Read<Product>("select * from Product where  Id=@prm0", id) ?? new Product();
		}
    }
}

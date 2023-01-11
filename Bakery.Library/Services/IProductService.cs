using Bakery.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Services
{
    public interface IProductService
    {
        List<Product> GetList();
        Product GetById(int id);
        void Create(Product product);
        void Delete(int id);
    }
}

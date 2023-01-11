using OrionDAL.Web.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Entity
{
    public class Admin : BaseEntity
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = ""; 

    }
}

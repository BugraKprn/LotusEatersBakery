using OrionDAL.OAL.Metadata;
using OrionDAL.Web.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Entity
{
    public class Product : BaseEntity
    {
        public int ProductOrder { get; set; }
        public string ProductName { get; set; } = "";
        public string Image { get; set; } = "";
        public string BigImage { get; set; } = "";
        public decimal Price { get; set; } = 0;
        [FieldDefinition(Length = 0)]
        public string Description { get; set; } = "";
        [FieldDefinition(Length = 0)]
        public string Allergen { get; set; } = "";
    }
}

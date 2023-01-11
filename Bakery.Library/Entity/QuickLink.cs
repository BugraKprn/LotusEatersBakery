using OrionDAL.OAL.Metadata;
using OrionDAL.Web.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Entity
{
    public class QuickLink : BaseEntity
    {
        [FieldDefinition(Length = 4000)]
        public string Url { get; set; } = "";
        public int LinkOrder { get; set; } = 0;
        [FieldDefinition(Length = 200)]
        public string DisplayName { get; set; } = "";
    }
}

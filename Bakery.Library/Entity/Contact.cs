using OrionDAL.OAL.Metadata;
using OrionDAL.Web.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Entity
{
    public class Contact : BaseEntity
    {
        [FieldDefinition(Length = 100)]
        public string ContactName { get; set; }
        [FieldDefinition(Length = 100)]
        public string ContactMail { get; set; }
        [FieldDefinition(Length = 50)]
        public string ContactSubject { get; set; }

        [FieldDefinition(Length = 0)]
        public string ContactMessage { get; set; }
    }
}

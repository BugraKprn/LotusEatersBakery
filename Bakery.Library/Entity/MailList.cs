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
    public class MailList : BaseEntity
    {
        [Required]
        [FieldDefinition(Length = 400)]
        public string Mail { get; set; } = "";
    }
}

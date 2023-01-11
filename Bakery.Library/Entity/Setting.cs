using OrionDAL.OAL.Metadata;
using OrionDAL.Web.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Entity
{
    public class Setting : BaseEntity
    {
        [FieldDefinition(Length = 200)]
        public string Phone { get; set; } = "";
        [FieldDefinition(Length = 400)]
        public string Mail { get; set; } = "";
        [FieldDefinition(Length = 400)]
        public string Facebook { get; set; } = "";
        [FieldDefinition(Length = 400)]
        public string Twitter { get; set; } = "";
        [FieldDefinition(Length = 400)]
        public string GooglePlus { get; set; } = "";
        [FieldDefinition(Length = 400)]
        public string Linkedin { get; set; } = "";
        [FieldDefinition(Length = 400)]
        public string Instagram { get; set; } = "";
        public string Logo { get; set; } = "";
        public string FooterLogo { get; set; } = "";
        public bool IsMailListShow { get; set; } = false;
        [FieldDefinition(Length = 255)]
        public string CompanyName { get; set; } = "";
        [FieldDefinition(Length = 4000)]
        public string ShortDescription { get; set; } = "";
        [FieldDefinition(Length = 1000)]
        public string WorkTime { get; set; } = "";
        [FieldDefinition(Length = 2000)]
        public string Address { get; set; } = "";
        [FieldDefinition(Length = 0)]
        public string KVKK { get; set; } = "";
        [FieldDefinition(Length = 0)]
        public string About{ get; set; } = "";
        [FieldDefinition(Length = 0)]
        public string Refund { get; set; } = "";
        [FieldDefinition(Length = 0)]
        public string Terms { get; set; } = "";

    }
}

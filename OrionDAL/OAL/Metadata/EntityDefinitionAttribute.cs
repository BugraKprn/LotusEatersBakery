using System;
using System.Collections.Generic;
using System.Text;

namespace OrionDAL.OAL.Metadata
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class EntityDefinitionAttribute : Attribute
    {
        public EntityDefinitionAttribute()
        {
        }

        private IdMethod idMethod = IdMethod.Identity;
        public IdMethod IdMethod
        {
            get { return idMethod; }
            set { idMethod = value; }
        }

        private string stringField;
        public string StringField
        {
            get { return stringField; }
            set { stringField = value; }
        }

        /// <summary>
        /// Optimistick Locking i?in kullan?lacak alan?n ismi.
        /// E?er b?yle bir alan yoksa null b?rak?lmal?.
        /// </summary>
        public string OptimisticLockField { get; set; }

        private EntityKind entityKind = EntityKind.Evrak;
        public EntityKind EntityKind
        {
            get { return entityKind; }
            set { entityKind = value; }
        }
    }     
}
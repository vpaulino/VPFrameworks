using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Framework
{
     
    public class EntityException   
    {
        public object Entity { get; set; }
 
        public EntityException()
        {

        }

        public EntityException(object entity) 
        {
            this.Entity = entity;
        }
        public EntityException(object entity, string message) 
            : base(message)
        {
            this.Entity = entity;
        }
        public EntityException(object entity, string message, Exception inner)
            : base(message, inner)
        {
            this.Entity = entity;
        }
        //protected EntityException(
        //  System.Runtime.Serialization.SerializationInfo info,
        //  System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context)
        //{

        //}

        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    base.GetObjectData(info, context);
        //}
    }
}

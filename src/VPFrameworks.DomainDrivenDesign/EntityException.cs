using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Framework
{
     /// <summary>
     /// 
     /// </summary>
    public class EntityException   : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public object Entity { get; set; }
 

        /// <summary>
        /// 
        /// </summary>
        public EntityException()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public EntityException(object entity) 
        {
            this.Entity = entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        public EntityException(object entity, string message) 
             
        {
            this.Entity = entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <param name="inner"></param>
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

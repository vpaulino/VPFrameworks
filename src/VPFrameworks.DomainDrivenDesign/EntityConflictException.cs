using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Framework
{
   /// <summary>
   /// If two entities have conflitcs
   /// </summary>
    public class EntityConflictException : EntityException
    {
         
       /// <summary>
       /// Creates instance
       /// </summary>
       /// <param name="entity"></param>

        public EntityConflictException(object entity) : base(entity)
        {
        
        }

        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>

        public EntityConflictException(object entity, string message)
            : base(entity, message)
        {

        }

        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public EntityConflictException(object entity, string message, Exception inner) : base(entity, message, inner)
        {
        
        }

        //protected EntityConflictException(
        //  System.Runtime.Serialization.SerializationInfo info,
        //  System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context) 
        //{
        
        //}
    }
}

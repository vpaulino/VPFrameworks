using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Framework
{
    /// <summary>
    /// entity represents the errors of two entities existing with the same id
    /// </summary>
    public class EntityDuplicateException : EntityException
    {

        /// <summary>
        /// Creates an instance of EntityDuplicateException
        /// </summary>
        /// <param name="entity"></param>
        public EntityDuplicateException(object entity) : base(entity)
        {

        }


        /// <summary>
        /// Creates an instance of EntityDuplicateException
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        public EntityDuplicateException(object entity, string message)
            : base(entity, message)
        {

        }
        

        /// <summary>
        /// Creates an instance of EntityDuplicateException
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public EntityDuplicateException(object entity, string message, Exception inner) : base(entity, message, inner)
        {
        
        }

        //protected EntityDuplicateException(
        //  System.Runtime.Serialization.SerializationInfo info,
        //  System.Runtime.Serialization.StreamingContext context)
        //    : base(info, context) 
        //{
        
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Framework
{
    
    public class EntityDuplicateException : EntityException
    {
        public override int InternalExceptionCode
        {
            get
            {
                return AAException.EntityDuplicateExceptionCode;
            }
        }

        public EntityDuplicateException(object entity) : base(entity)
        {

        }

        public EntityDuplicateException(object entity, string message)
            : base(entity, message)
        {

        }

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
